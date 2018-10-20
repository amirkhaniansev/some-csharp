using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DatabaseCreator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input the server name");
            var msg = Console.ReadLine();

            try
            {
                var scripts = new[]
                {
                    File.ReadAllText(Path.GetFullPath("CreateDatabase.sql")),
                    File.ReadAllText(Path.GetFullPath("CreateBooksTable.sql")),
                    File.ReadAllText(Path.GetFullPath("InsertBooks.sql"))
                };

                var sqlConenctionStringBuilder = new SqlConnectionStringBuilder
                {
                    IntegratedSecurity = true,
                    DataSource = msg
                };

                var sqlConnection = new SqlConnection(sqlConenctionStringBuilder.ConnectionString);

                sqlConnection.Open();

                foreach (var script in scripts)
                {
                    var sqlCommand = new SqlCommand
                    {
                        CommandText = script,
                        CommandType = CommandType.Text,
                        Connection = sqlConnection
                    };

                    sqlCommand.ExecuteNonQuery();
                }

                Console.WriteLine("Operation ended successfully");
            }
            catch (Exception)
            {
                Console.WriteLine("Error occured.Please try again");
            }

            Console.ReadLine();
        }
    }
}
