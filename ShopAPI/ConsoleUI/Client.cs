using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleUI
{
    /// <summary>
    /// Client class 
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Base address URI
        /// </summary>
        private readonly Uri uri;

        /// <summary>
        /// Http client.
        /// </summary>
        private readonly HttpClient httpClient;

        /// <summary>
        /// Creates new instance of Client.
        /// </summary>
        public Client()
        {
            //Constrcuting URI address.
            var uriString = System.Configuration
                .ConfigurationManager.AppSettings["baseAddress"];

            this.uri = new Uri(uriString);

            //Constructing http client
            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = this.uri;
            this.httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets products.
        /// </summary>
        /// <returns> Returns an enumerable of products. </returns>
        public IEnumerable<ClientProduct> Get()
        {
            //creating request and getting response
            var response = this.httpClient.GetAsync("api/product").Result;

            //getting result
            var result = response.Content.ReadAsStringAsync().Result;

            //Converting from JSON to enumerable of products
            return Parser.ParseFromJson(result);
        }

        /// <summary>
        /// Sends POST request to Web Service.
        /// </summary>
        /// <param name="clientProduct"> Client product.</param>
        /// <returns> Returns status describing the operation. </returns>
        public Status Post(ClientProduct clientProduct)
        {
            //creating content for request
            var content = Parser.ConvertToContent(clientProduct);

            //creating request and getting response
            var response = this.httpClient.PostAsync("api/product", content).Result;            

            //if the request is successfull ,then return Status.Success
            if (response.IsSuccessStatusCode)
            {
                return Status.Success;
            }

            //otherwise return Status.Failure
            return Status.Failure;
        }

        /// <summary>
        /// Sends PUT request to Web Service.
        /// </summary>
        /// <param name="clientProduct"> Client product.</param>
        /// <returns> Returns status describing the operation. </returns>
        public Status Put(ClientProduct clientProduct)
        {
            //Making Content form product
            var content = Parser.ConvertToContent(clientProduct);

            //Sending PUT request and getting response result
            var respone = this.httpClient.PutAsync("api/product", content).Result;

            //if the request is successfully performed then return Status.Success
            if (respone.IsSuccessStatusCode)
            {
                return Status.Success;
            }

            //otherwise return Status.Failure
            return Status.Failure;
        }

        /// <summary>
        /// Deletes the product with the given id.
        /// </summary>
        /// <param name="id"> Product ID.</param>
        /// <returns> Returns status describing the operation. </returns>
        public Status Delete(int id)
        {
            //Sending DELETE request and getting response
            var response = this.httpClient.DeleteAsync($"api/product/{id}").Result;
          
            //if the request is successfully performed then return Status.Success
            if (response.IsSuccessStatusCode)
            {
                return Status.Success;
            }

            //otherwise return Status.Failure
            return Status.Failure;
        }

        /// <summary>
        /// Runs the client
        /// </summary>
        public void Run()
        {
            //Message about the client start
            Console.WriteLine("Client started running...");

            //declaring variables for input/output purposes
            var initialInput = "";
            var input = "";
            var productString = "";
            var product = new ClientProduct();
            var status = new Status();
            var products = null as IEnumerable<ClientProduct>;
            var id = 0;

            while (true)
            {
                Console.WriteLine("Enter add,get,update or delete for processing products");
                Console.WriteLine("Enter exit in case you want to exit.");

                //input
                initialInput = Console.ReadLine();

                //processing input
                input = initialInput.Replace(" ", "").ToLower();

                try
                {
                    //if the user entered add ,then add new product
                    if (input == "add")
                    {
                        //message
                        Console.WriteLine("Enter new product in this format:  ID:Name:Category:Price");

                        //input
                        productString = Console.ReadLine();

                        //converting input to product
                        product = Parser.ParseFromString(productString);

                        //sending POST request
                        status = this.Post(product);

                        //Printing operation status to Console
                        Console.WriteLine(status);

                    }

                    //if the user entered get,then get all products
                    if (input == "get")
                    {
                        
                        //getting products
                        products = this.Get();
    

                        //printing the products to Console
                        foreach (var counter in products)
                        {
                            Console.WriteLine(counter);
                        }
                    }

                    //if the user entered update,then update the product
                    if (input == "update")
                    {
                        //Message
                        Console.WriteLine(
                            "For updating product enter it in this format: ID:NewName:NewCategory:NewPrice");

                        //input
                        productString = Console.ReadLine();

                        //Converting from input string to product
                        product = Parser.ParseFromString(productString);

                        //Updating the product and getting the status of operation
                        status = this.Put(product);

                        //printing the status of operation
                        Console.WriteLine(status);
                    }

                    //if the user entered delete,then delete the product
                    if (input == "delete")
                    {
                        //Message
                        Console.WriteLine("For deleting product enter id");

                        //id input
                        id = int.Parse(Console.ReadLine());

                        //getting status after delete
                        status = this.Delete(id);

                        //printing operation status to Console
                        Console.WriteLine(status);
                    }

                    //if the user entered exit,close the application
                    if (input == "exit")
                    {
                        break;
                    }
                }

                //catching Format exception
                //this will be catched if the user entered invalid input
                catch (FormatException)
                {
                    //printing input error message 
                    Console.WriteLine("Invalid input");
                }

                Console.WriteLine();
            }
        }

    }
}