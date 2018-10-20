using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;


/*---------------------

Here I use the modified version of DataAccessLayer written by me.
For more information see : https://github.com/amirkhaniansev/DataAccessLayer

---------------------*/

namespace BookstoreService
{
    /// <summary>
    /// Class for accessing database
    /// </summary>
    internal static class DatabaseAccess
    {
        /// <summary>
        /// Adds the book to database.
        /// </summary>
        /// <param name="book"> Book. </param>
        internal static void Add(Book book)
        {
            //creating operation info for managing operation
            var operationInfo = DatabaseAccess.CreateOperationInfo("AddBook");
            
            //building sql connection string
            var sqlConnectionString = DatabaseAccess.BuildConnectionString(operationInfo);

            //opening connection and doing operations
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                //building parameters
                var parameters = DatabaseAccess.BuildParameters(
                    new []
                    {
                        new KeyValuePair<string, object>("ID", book.ID),
                        new KeyValuePair<string, object>("Author", book.Author),
                        new KeyValuePair<string, object>("Title", book.Title),
                        new KeyValuePair<string, object>("Price", book.Price),
                        new KeyValuePair<string, object>("Year", book.Year)
                    }, operationInfo);

                //building command
                var sqlCommand = DatabaseAccess.BuildCommand(sqlConnection, parameters, operationInfo);

                //opening connection
                sqlConnection.Open();

                try
                {
                    var rowsAffected = sqlCommand.ExecuteNonQuery();
                }
                catch (SqlException)
                {
                    throw new InvalidOperationException("ID");
                }
            }
        }

        /// <summary>
        /// Updates the prices of the book.
        /// </summary>
        /// <param name="priceUpdateInfo"> Book price update protocol text. </param>
        internal static void UpdateBookPrice(PriceUpdateInfo priceUpdateInfo)
        {
            //creating operation info for managing operation
            var operationInfo = DatabaseAccess.CreateOperationInfo("UpdateBookPrice");

            //building sql connection string
            var sqlConnectionString = DatabaseAccess.BuildConnectionString(operationInfo);

            //opening connection and doing operatiosn
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                var parameters = DatabaseAccess.BuildParameters(
                    new[]
                    {
                        new KeyValuePair<string, object>("Price", priceUpdateInfo.NewPrice),
                        new KeyValuePair<string, object>("ID", priceUpdateInfo.ID)
                    }, operationInfo);

                //building command
                var sqlCommand = DatabaseAccess.BuildCommand(sqlConnection, parameters, operationInfo);

                sqlConnection.Open();

                try
                {
                    var rowsAffected = sqlCommand.ExecuteNonQuery();                   
                }
                catch (SqlException)
                {
                    throw new InvalidOperationException("No book with that ID");
                }
            }
        }

        /// <summary>
        /// Gets books from the database.
        /// </summary>
        /// <returns> Returns the books of database. </returns>
        internal static List<Book> GetBooks()
        {
            //creating operation info for managing operation
            var operationInfo = DatabaseAccess.CreateOperationInfo("GetBooks");

            //building sqlConnection
            var sqlConnectionString = DatabaseAccess.BuildConnectionString(operationInfo);

            //list for storing the books
            var listOfBooks = new List<Book>();

            //opening connection and doing operations
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                //building command
                var sqlCommand = DatabaseAccess.BuildCommand(sqlConnection, null, operationInfo);

                sqlConnection.Open();

                //reading the result
                using (var reader = sqlCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //constructing an instance of book
                        var book = new Book
                        {
                            ID = (int)reader["ID"],
                            Author = (string) reader["Author"],
                            Title = (string) reader["Title"],
                            Price = (double) reader["Price"],
                            Year = (int) reader["Year"]
                        };

                        //adding the book to list
                        listOfBooks.Add(book);
                    }
                }                  
            }

            return listOfBooks;
        }

        /// <summary>
        /// Creates operation info for operation.
        /// </summary>
        /// <param name="operationMappedName"> Mapped name of operation. </param>
        /// <returns> Returns the operation info.</returns>
        private static OperationInfo CreateOperationInfo(string operationMappedName)
        {
            try
            {
                //getting xml document of operations
                var filePath = Path.GetFullPath("Operations.xml");
                var opXML = XDocument.Load(filePath).XPathSelectElement(
                    "//operation[@name='" + operationMappedName + "']");

                //constructing operation info
                var operationInfo = new OperationInfo();
                operationInfo.OperationName = operationMappedName;
                operationInfo.OperationType = opXML.Element("operationType").Value;
                operationInfo.SQL_ServerName = opXML.Element("sqlServerName").Value;
                operationInfo.DatabaseName = opXML.Element("databaseName").Value;
                operationInfo.OperationCode = opXML.Element("operationCode").Value;
                operationInfo.ParametersList = new List<string>();

                //Add parameters only when the operation is not GetBooks 
                if (operationInfo.OperationName != "GetBooks")
                {
                    operationInfo.ParametersList.AddRange(opXML.Element("parameters").Elements("parameter").Select(
                        opxml => opxml.Value));
                }

                //return operation info
                return operationInfo;
            }

            //if anyhow operation wasn't able to be constructed then return null
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Builds connection string
        /// </summary>
        /// <param name="operationInfo"> Operation info.</param>
        /// <returns> Returns the connection string for the specific operation info.</returns>
        private static string BuildConnectionString(OperationInfo operationInfo)
        {
            //constructing connection string
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
            {
                DataSource = operationInfo.SQL_ServerName,
                InitialCatalog = operationInfo.DatabaseName,
                IntegratedSecurity = true
            };

            //return connection string
            return sqlConnectionStringBuilder.ConnectionString;
        }

        /// <summary>
        /// Builds the command.
        /// </summary>
        /// <param name="connection"> Connection.</param>
        /// <param name="sqlParameters"> Parameters. </param>
        /// <param name="operationInfo"> Operation info. </param>
        /// <returns> Returns the command. </returns>
        private static SqlCommand BuildCommand(SqlConnection connection, SqlParameter[] sqlParameters,OperationInfo operationInfo)
        {
            //constructing command
            var sqlCommand = new SqlCommand();
            sqlCommand.CommandText = operationInfo.OperationCode;
            sqlCommand.Connection = connection;
            sqlCommand.CommandType = (CommandType)Enum.Parse(typeof(CommandType),
                operationInfo.OperationType);

            //if there is no parameters(for example "GetBooks") ,then parameters must not be added to sql command.
            if (sqlParameters != null)
            {
                sqlCommand.Parameters.AddRange(sqlParameters);
            }

            //return command
            return sqlCommand;
        }

        /// <summary>
        /// Creates the sequence of parameters.
        /// </summary>
        /// <param name="parameters"> Paramters given with KeyValuePair.</param>
        /// <param name="operationInfo"> Operation info. </param>
        /// <returns> Returns the sequence of parameters. </returns>
        private static SqlParameter[] BuildParameters(KeyValuePair<string, object>[] parameters,OperationInfo operationInfo)
        {
            //constructing the array of paramaters
            var sqlParameters = new SqlParameter[parameters.Length];

            for (var counter = 0; counter < sqlParameters.Length; counter++)
            {
                //constrcuting parameters
                var parameterInfo = operationInfo.ParametersList[counter].Split(new[] {' '},
                    StringSplitOptions.RemoveEmptyEntries);
                sqlParameters[counter] = new SqlParameter();
                sqlParameters[counter].ParameterName = parameterInfo[0];
                sqlParameters[counter].Value = parameters[counter].Value;
                sqlParameters[counter].SqlDbType = (SqlDbType)Enum.Parse(typeof(SqlDbType), parameterInfo[2]);           
            }

            //return the parameters
            return sqlParameters;
        }

    }
}