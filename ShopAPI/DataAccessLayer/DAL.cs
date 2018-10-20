using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Configuration;

namespace DataAccessLayer
{
    /// <summary>
    /// Data Access Layer
    /// </summary>
    public class DAL
    {
        /// <summary>
        /// Database connection string
        /// </summary>
        private readonly string sqlConnectionString;

        /// <summary>
        /// Creates an instance of data access layer
        /// </summary>
        public DAL()
        {
            //Getting connection string from Web API web.config
            this.sqlConnectionString = ConfigurationManager
                .ConnectionStrings["ShopDbConnection"].ConnectionString;
        }

        /// <summary>
        /// Gets products from database.
        /// </summary>
        /// <returns> Returns the list of products. </returns>
        public IEnumerable<ProductData> GetProducts()
        {
            //list of products
            var listOfProducts = new List<ProductData>();

            //accessing database
            using (var sqlConnection = new SqlConnection(this.sqlConnectionString))
            {
                //opening sql connection
                sqlConnection.Open();

                //constructing sql command
                var sqlCommand = new SqlCommand
                {
                    CommandText = "Select * from Products",
                    Connection = sqlConnection
                };

                //executing reader
                using (var reader = sqlCommand.ExecuteReader())
                {
                    //if there are products in database
                    if (reader.HasRows)
                    {
                        //reading rows
                        while (reader.Read())
                        {
                            //constructing product data
                            var productData = new ProductData
                            {
                                ID = (int) reader["ID"],
                                Name = (string) reader["Name"],
                                Category = (string) reader["Category"],
                                Price = (double) reader["Price"]
                            };

                            //adding the product to the list
                            listOfProducts.Add(productData);
                        }
                    }
                    else
                    {
                        throw new FailedDalOperationException("No products");
                    }
                }
            }

            //returning list of products
            return listOfProducts;
        }

        /// <summary>
        /// Adds new product to database.
        /// </summary>
        /// <param name="productData"> Data about Product.</param>
        public void AddProduct(ProductData productData)
        {
            //accessing database
            using (var sqlConnection = new SqlConnection(this.sqlConnectionString))
            {
                //opening connection
                sqlConnection.Open();

                //constructing sql command
                var sqlCommand = new SqlCommand
                {
                    CommandText = @"use [Shop]
                                    insert into Products values(@ID,@Name,@Category,@Price)",
                    Connection = sqlConnection,
                };

                //adding parameters to sql command
                var parameters = this.ConstructParameters(productData);
                sqlCommand.Parameters.AddRange(parameters);


                //this slice of code is in try catch becuase it is possible that
                //there will be attempt to add product with already existing ID
                try
                {
                    sqlCommand.ExecuteNonQuery();
                }

                //catching sql exception
                catch (SqlException)
                {
                    throw new FailedDalOperationException("Product with that ID already exists.");
                }

            }
        }

        /// <summary>
        /// Constructs the parameters.
        /// </summary>
        /// <param name="productData"> Data about Product. </param>
        /// <returns> Returns constructed parameters .</returns>
        private SqlParameter[] ConstructParameters(ProductData productData)
        {
            //paramaters
            var sqlParameters = new SqlParameter[4];

            //constructing id
            var id = new SqlParameter
            {
                ParameterName = "ID",
                Value = productData.ID,
                SqlDbType = SqlDbType.Int
            };

            //adding id to paramaters' list
            sqlParameters[0] = id;

            //constructing name
            var name = new SqlParameter
            {
                ParameterName = "Name",
                Value = productData.Name,
                SqlDbType = SqlDbType.NVarChar
            };

            //adding name to parameters' list
            sqlParameters[1] = name;

            //constructing category
            var category = new SqlParameter
            {
                ParameterName = "Category",
                Value = productData.Category,
                SqlDbType = SqlDbType.NVarChar
            };

            //adding category to paramters' list
            sqlParameters[2] = category;

            //constructing price
            var price = new SqlParameter
            {
                ParameterName = "Price",
                Value = productData.Price,
                SqlDbType = SqlDbType.Float
            };

            //adding price to parameters' list
            sqlParameters[3] = price;

            //returns sql command parameters
            return sqlParameters;
        }

        /// <summary>
        /// Deletes product.
        /// </summary>
        /// <param name="id">ID of the product.</param>
        public void DeleteProduct(int id)
        {
            var rowsAffected = 0;

            //accessing database
            using (var sqlConnection = new SqlConnection(this.sqlConnectionString))
            {
                //opening connection
                sqlConnection.Open();

                //constructing sql command
                var sqlCommand = new SqlCommand
                {
                    CommandText = "Delete from Products where ID = @ID",
                    Connection = sqlConnection
                };

                //adding parameter to sql Command
                sqlCommand.Parameters.Add(
                    new SqlParameter
                    {
                        ParameterName = "ID",
                        Value = id,
                        SqlDbType = SqlDbType.Int,
                    });

                //executing sql command
                rowsAffected = sqlCommand.ExecuteNonQuery();

            }

            if (rowsAffected == 0)
            {
                throw new FailedDalOperationException("No product with that ID.");
            }
        }

        /// <summary>
        /// Updates product.
        /// </summary>
        /// <param name="newProductData">New product data.</param>
        public void UpdateProduct(ProductData newProductData)
        {
            var rowsAffected = 0;

            //accessing database
            using (var sqlConnection = new SqlConnection(this.sqlConnectionString))
            {
                //opening sql connection
                sqlConnection.Open();

                //constructing sql command
                var sqlCommand = new SqlCommand
                {
                    CommandText = @"update Products 
                                    set [Name] = @Name,Category = @Category,Price = @Price
                                    where ID = @ID",
                    Connection = sqlConnection
                };

                var parameters = this.ConstructParameters(newProductData);

                sqlCommand.Parameters.AddRange(parameters);

                //executing sql command
                rowsAffected = sqlCommand.ExecuteNonQuery();

            }

            if (rowsAffected == 0)
            {
                throw new FailedDalOperationException("No product with that ID.");
            }
        }
    }
}
