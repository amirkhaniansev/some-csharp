using System;
using Client.BookstoreServiceReference;

namespace Client
{
    public  class ClientRunner
    {
        private BookstoreServiceClient client;

        public ClientRunner()
        {
            this.client = new BookstoreServiceClient();

        }

        public void Run()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Client app started at time: " + DateTime.Now);
            while (true)
            {
                Console.ResetColor();
                Console.WriteLine("Type the name of operation you'd like." + Environment.NewLine +
                                  "Add: Adds a new book to database." + Environment.NewLine +
                                  "Update: Updates the price of the book" + Environment.NewLine +
                                  "Get: Gets all the books from database" + Environment.NewLine +
                                  "Close: Closes the client app.");

                var clientMessage = Console.ReadLine();
                var msg = clientMessage.Replace(" ", "").ToLower();
                var result = new Result();

                if (msg == "add")
                {
                    result = this.Add();
                }

                if (msg== "update")
                {
                    result = this.Update();
                }

                if (msg == "get")
                {
                    result = this.Get();
                }

                if(msg == "close")
                    break;

                if (!result.Equals(default(Result)))
                {
                    if (result.Status == Status.Success)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                    }
                    Console.WriteLine("Status: " + result.Status + "   Message: " + result.Message);
                    Console.ResetColor();
                }

            }

            this.client.Close();
        }

        private Result Add()
        {
            Console.WriteLine("Type the book information in this format:" + Environment.NewLine + 
                              "ID:Author:Title:Price:Year");
            var msg = Console.ReadLine();
            var result = this.client.AddBook(msg);
            return result;
        }

        private Result Update()
        {
            Console.WriteLine("Type the price update information in this format:" + Environment.NewLine +
                              "ID:New Price");
            var msg = Console.ReadLine();
            var result = this.client.UpdateBookPrice(msg);
            return result;
        }

        private Result Get()
        {
            var list = this.client.GetBooks(out var result);

            if (list != null)
            {
                foreach (var counter in list)
                {
                    Console.WriteLine(counter.ID + " " + counter.Author + " " + counter.Title + " " +
                                      counter.Price + " " + counter.Year);
                }
            }
           
            return result;
        }
    }
}