using System.Data;
using System.Data.SqlClient;

namespace AutoFramework.Helpers
{
    public static class DataHalperExtensions
    {
        //Open the connection with the DB 
        public static SqlConnection DBConnect(this SqlConnection sqlConnection, string connectionString)
        {
            try
            {
                sqlConnection = new SqlConnection(connectionString); 
                sqlConnection.Open();
                return sqlConnection;
            }
            catch (Exception e)
            {
                LogHelpers.Write($"Error: {e.Message} ");
            }
            return null;
        }

        //Closing the connection with the DB
        public static void DBClose(this SqlConnection sqlConnection)
        {
            try
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
            }
            catch (Exception e)
            {
                LogHelpers.Write($"Error: {e.Message} ");
            }
        }

        //Execution
        public static DataTable ExecuteQuery(this SqlConnection sqlConnection, string queryString)
        {
            DataSet dataset;

            try
            {
                //Checking the state of connection
                if(sqlConnection == null || ((sqlConnection != null) && (sqlConnection.State == ConnectionState.Closed || sqlConnection.State == ConnectionState.Broken)))
                {
                    sqlConnection.Open();
                }

                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.SelectCommand = new SqlCommand(queryString, sqlConnection);
                dataAdapter.SelectCommand.CommandType = CommandType.Text;

                dataset = new DataSet();
                dataAdapter.Fill(dataset, "table");
                sqlConnection.Close();

                return dataset.Tables["table"];
            }
            catch(Exception e)
            {
                dataset = null;
                sqlConnection.Close();
                LogHelpers.Write($"Error: {e.Message} ");
                return null;
            }
            finally
            {
                sqlConnection.Close();
                dataset = null;
            }

        }

    }
}
