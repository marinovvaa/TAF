using System.Data;
using ExcelDataReader;

namespace AutoFramework.Helpers
{
    public class ExcelHelper
    {
       
        private static List<DataCollection> _dataCollection = new List<DataCollection>();   

        /// <summary>
        /// Read all the data from ExcelToDataTable() which returns DataTable and from this table it is 
        /// going to iterate to get all the values and then is going to store them into the dataCollection
        /// </summary>
        public static void PopulateCollection(string fileName)
        {
            DataTable table = ExcelToDataTable(fileName);

            //Iterate through the rows and columns of the table
            for(int row = 1; row <= table.Rows.Count; row++)
            {
                for(int col = 0; col < table.Columns.Count; col++)
                {
                    DataCollection dtTable = new DataCollection()
                    {
                        rowNumber = row,
                        colName = table.Columns[col].ColumnName,
                        colValue = table.Rows[row - 1][col].ToString()
                    };
                    _dataCollection.Add(dtTable);
                }
            }
        }

        //Reads all the data from the excel sheet and returns the Sheet as a DataTable
        private static DataTable ExcelToDataTable(string fileName)
        {
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            //open file and returns as Stream
            using var stream = File.Open(fileName, FileMode.Open, FileAccess.Read);

            using var reader = ExcelReaderFactory.CreateReader(stream);

            var result = reader.AsDataSet(new ExcelDataSetConfiguration()
            {
                ConfigureDataTable = (data => new ExcelDataTableConfiguration()
                {
                    UseHeaderRow = true
                })
            });

            //Get all the tables
            DataTableCollection table = result.Tables;
            //Store it in DataTable
            DataTable resultTable = table["Sheet1"];

            //return 
            return resultTable;
        }
        /// <summary>
        /// Reads data from the data collection 
        /// </summary>        
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retrieving data using LINQ to reduce much of iterations
                string data = (from colData in _dataCollection
                               where colData.colName == columnName && colData.rowNumber == rowNumber
                               select colData.colValue).SingleOrDefault();

                return data.ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
   
    //Custom class that has rowNumber, colName and colValue, it stores the information about them 
    public class DataCollection
    {
        public int rowNumber { get; set; }  

        public string colName { get; set; }

        public string colValue { get; set; } 
    }
}
