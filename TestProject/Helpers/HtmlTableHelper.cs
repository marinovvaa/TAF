using OpenQA.Selenium;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFramework.Helpers
{
    public class HtmlTableHelper
    {
        private static List<TableDataCollection> _tableDataCollections;


        public static void ReadTable(IWebElement table)
        {
            //Initialize the table
            _tableDataCollections = new List<TableDataCollection>();

            //Get all the columns from the table
            var columns = table.FindElements(By.TagName("th"));

            //Get all the rows from the table
            var rows = table.FindElements(By.TagName("tr"));

            //Create row index
            int rowIndex = 0;
            foreach(var row in rows)
            {
                int colIndex = 0;
                int headerIndex = 0; 

                var colDatas = row.FindElements(By.TagName("td"));
                //Store data only if it has value in row
                if (colDatas.Count != 0)
                    foreach (var colValue in colDatas)
                    {
                        _tableDataCollections.Add(new TableDataCollection
                        {
                            RowNumber = rowIndex,
                            ColumnName = columns[headerIndex].Text != "" ?
                                         columns[headerIndex].Text : colIndex.ToString(),
                            ColumnValue = colValue.Text,
                            ColumnSpecialValues = GetControl(colValue)
                        });

                        //Move to next column
                        colIndex++;
                        //header may not have the same # of columns as table data does
                        //stop incrementing the header index when the index once last column is reached
                        headerIndex = colIndex < columns.Count ? colIndex : headerIndex;
                    }
                //Move to next row
                rowIndex++;
            }

        }

        private static ColumnSpecialValue GetControl(IWebElement columnValue)
        {
            ColumnSpecialValue columnSpecialValue = null;
            //Check if the control has specfic tags like input/hyperlink etc
            //We can use enum for setting control type
            if (columnValue.FindElements(By.TagName("a")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("a")),
                    ControlType = "hyperLink"
                };
            }
            if (columnValue.FindElements(By.TagName("input")).Count > 0)
            {
                columnSpecialValue = new ColumnSpecialValue
                {
                    ElementCollection = columnValue.FindElements(By.TagName("input")),
                    ControlType = "input"
                };
            }

            return columnSpecialValue;
        }

        /// <summary>
        /// First we get the row number by refColumnName and refColumnValue, then we search for the cell which mathces the column index and rowNumber.
        /// If the controlToOperate and rowNumber is different than null we search for the control which text matches the controlToOperate
        /// and after that we click on it. We have 2 diffetent if statements for handling hyperlink and input 
        /// If controlToOperate is null it performs the click operation on the first value
        /// </summary>    
         public static void PerformActionOnCell(string columnIndex, string refColumnName, string refColumnValue, string controlToOperate = null)
        {
            foreach (int rowNumber in GetDynamicRowNumber(refColumnName, refColumnValue))
            {
                var cell = (from e in _tableDataCollections
                            where e.ColumnName == columnIndex && e.RowNumber == rowNumber
                            select e.ColumnSpecialValues).SingleOrDefault();

                //Operating on those controls
                if (controlToOperate != null && cell != null)
                {
                    //Since based on the control type, the retriving of text changes
                    //created this kind of control
                    if (cell.ControlType == "hyperLink")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.Text == controlToOperate
                                               select c).SingleOrDefault();

                        //ToDo: Currenly only click is supported, future is not taken care here
                        returnedControl?.Click();
                    }
                    if (cell.ControlType == "input")
                    {
                        var returnedControl = (from c in cell.ElementCollection
                                               where c.GetAttribute("value") == controlToOperate
                                               select c).SingleOrDefault();

                        //ToDo: Currenly only click is supported, future is not taken care here
                        returnedControl?.Click();
                    }

                }
                else
                {
                    cell.ElementCollection?.First().Click();
                }
            }
        }

        /// <summary>
        /// Gets the row number by using columnName and ColumnValue by searching in the table collection 
        /// </summary>        
        private static IEnumerable GetDynamicRowNumber(string columnName, string columnValue)
        {
            //dynamic row
            foreach (var table in _tableDataCollections)
            {
                if (table.ColumnName == columnName && table.ColumnValue == columnValue)
                    yield return table.RowNumber;
            }
        }

    }

    public class TableDataCollection
    {
        public int RowNumber { get; set; } 
        public string ColumnName { get; set; }  
        public string ColumnValue { get; set; } 
        public ColumnSpecialValue ColumnSpecialValues { get; set; }
    }

    public class ColumnSpecialValue
    {
        public IEnumerable<IWebElement> ElementCollection { get; set; }
        public string ControlType { get; set; } 
    }
}
