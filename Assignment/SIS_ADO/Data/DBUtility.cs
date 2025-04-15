using System;
using System.Data;
using System.Data.SqlClient;

namespace AdoConnectedDemo.Data
{
    internal class DBUtility
    {
        
        const string connectionString = @"Data Source=LAPTOP-V71FT21G\SQLEXPRESS; Initial Catalog=hexa; Integrated Security=True; MultipleActiveResultSets=true;";

        public static SqlConnection GetConnection()
        {
            SqlConnection connectionObject = new SqlConnection(connectionString);

            try
            {
                connectionObject.Open();
                return connectionObject;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error opening the connection: {e.Message}");
                return null;
            }
        }

        public static void CloseDbConnection(SqlConnection connectionObject)
        {
            if (connectionObject != null)
            {
                try
                {
                    if (connectionObject.State == ConnectionState.Open)
                    {
                        connectionObject.Dispose();
                        connectionObject.Close();
                        Console.WriteLine("Connection closed successfully.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error closing connection: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Connection is already null.");
            }
        }
    }
}

