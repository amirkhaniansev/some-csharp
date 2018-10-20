using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace BookstoreService
{
    /// <summary>
    /// Service for bookstore
    /// </summary>
    public class Bookstore : IBookstoreService
    {
        /// <summary>
        /// Runs bookstore service
        /// </summary>
        public void Run()
        {
            //endpoint address for binding
            var address = new Uri("http://localhost:8000/");

            //making host for bookstore service
            var host = new ServiceHost(typeof(Bookstore), address);

            //enabling http by adding specific behavior
            var behavior = new ServiceMetadataBehavior
            {
                HttpGetEnabled = true
            };
            host.Description.Behaviors.Add(behavior);

            //Opening host
            host.Open();
            
            Console.WriteLine("Service started running at " + Environment.NewLine + 
                              "Address: " + address + " Time: " + DateTime.Now);
            Console.WriteLine("Press <Enter> for ending service work.");

            Console.ReadLine();

            host.Close();
        }

        /// <summary>
        /// Adds book to database
        /// </summary>
        /// <param name="bookAddProtocolText"> Protocol text for adding book. </param>
        /// <returns> Returns result which indicates whether the operation ended successfully. </returns>
        public Result AddBook(string bookAddProtocolText)
        {
            //This following code must be in try-catch block
            //because there is probability of some exceptions
            try
            {
                var book = Parser.ParseToBook(bookAddProtocolText);
                DatabaseAccess.Add(book);
            }

            //Invalid protocol text
            catch (FormatException ex)
            {
                return new Result
                {
                    Status = Status.Fail,
                    Message = ex.Message
                };
            }

            //Invalid operation:for example trying to add a book with already existing ID
            catch (InvalidOperationException)
            {
                return new Result
                {
                    Status = Status.Fail,
                    Message = "Book with that ID already exists"
                };
            }

            //if everything is ok ,return the result indicating the success of operation
            return new Result
            {
                Status = Status.Success,
                Message = "The book was succesfully added to the database"
            };
        }

        
        /// <summary>
        /// Updates the price of book.
        /// </summary>
        /// <param name="priceUpdateProtocolText"> Book's price update protocol text.</param>
        /// <returns> Returns the result indicating whether the operation ended successfully.</returns>
        public Result UpdateBookPrice(string priceUpdateProtocolText)
        {
            //getting the result of operation
            try
            {
                var priceUpdateInfo = Parser.ParseToPriceUpdateInfo(priceUpdateProtocolText);
                DatabaseAccess.UpdateBookPrice(priceUpdateInfo);
            }
            catch (FormatException)
            {
                return new Result
                {
                    Status = Status.Fail,
                    Message = "Invalid protocol text."
                };
            }
            catch (SqlException)
            {
                return new Result
                {
                    Status = Status.Fail,
                    Message = "Book with that ID doesn't exist"
                };
            }

            return new Result
            {
                Status = Status.Success,
                Message = "Price of the book was succesfully updated"
            };
        }

        /// <summary>
        /// Gets all the books from database.
        /// </summary>
        /// <param name="result"> Indicates the success of operation.</param>
        /// <returns></returns>
        public List<Book> GetBooks(out Result result)
        {
            //Getting the books from database
            var list = DatabaseAccess.GetBooks();

            //if there are no books in database 
            //then return the inform the client about the failure with the result
            if (list == null)
            {
                result = new Result
                {
                    Status = Status.Fail,
                    Message = "No books in database"
                };
            }

            //otherwise inform the client about the success of operation
            else
            {
                result = new Result
                {
                    Status = Status.Success,
                    Message = "All books in database"
                };
            }

            //return list of books
            return list;
        }
    }
}
