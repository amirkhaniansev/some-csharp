using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    /// <summary>
    /// Product Controller
    /// </summary>
    public class ProductController : Controller
    {
        /// <summary>
        /// Service URI
        /// </summary>
        private Uri uri;

        public ProductController()
        {
            var uriString = System.Configuration
                .ConfigurationManager.AppSettings["baseAddress"];

            this.uri = new Uri(uriString);
        }

        /// <summary>
        /// Shows the list of products.
        /// </summary>
        /// <returns> Returns the show action result. </returns>
        public ActionResult Show()
        {
            //list of products
            var list = new List<Product>();

            // constructing http client
            var httpClient = new HttpClient();

            //constructing base address uri
            httpClient.BaseAddress = this.uri;

            //adding JSON support
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //sending GET request and getting response
            var response = httpClient.GetAsync("api/product").Result;

            //getting result
            var result = response.Content.ReadAsStringAsync().Result;

            //constrcuting list by deserializing JSON objects
            list = JsonConvert.DeserializeObject<List<Product>>(result);

            //returning view of lists
            return View(list);
        }

        /// <summary>
        /// Creates new product.
        /// </summary>
        /// <returns> Returns creation product result. </returns>
        public ActionResult Create()
        {
            //returns view
            return View();
        }

        /// <summary>
        /// Creates new product by sending POST request to Web API.
        /// </summary>
        /// <param name="product"> Product which will be added to database. </param>
        /// <returns>  Returns creation action result. </returns>
        [HttpPost]
        public ActionResult Create(Product product)
        {
            //if the model state is not valid
            //just return the view
            if (!this.ModelState.IsValid)
            {
                return View();
            }

            //constructing http client
            var httpClient = new HttpClient();

            //constructing base address URI
            httpClient.BaseAddress = this.uri;

            //adding JSON support
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //serializing product by making it JSON object
            var json = JsonConvert.SerializeObject(product);

            //sending POST request and getting the response
            var response = httpClient.PostAsync("api/product",
                new StringContent(json, Encoding.UTF8, "application/json")).Result;

            //redirection to Showing the lists of products
            return RedirectToAction("Show");
        }


        /// <summary>
        /// Deletes the product by the given id.
        /// </summary>
        /// <param name="id"> ID of the product. </param>
        /// <returns> Returns deletion action result. </returns>
        public ActionResult Delete(int id)
        {
            //constructing http client
            var httpClient = new HttpClient();
            
            //constructing base address URI
            httpClient.BaseAddress = this.uri;

            //adding JSON support
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            //sending DELETE request to Web API and getting the respone
            var response = httpClient.DeleteAsync($"api/product/{id}").Result;

            //if invalid request is sent
            if (!response.IsSuccessStatusCode)
            {
                //return http not found view
                return HttpNotFound();
            }

            //redirecting to showing the list of the products
            return RedirectToAction("Show");
        }

        /// <summary>
        /// Edits the product with the given ID.
        /// </summary>
        /// <param name="id"> Product ID. </param>
        /// <returns> Returns the edit action result. </returns>
        public ActionResult Edit(int id)
        {
            //returning view
            return View();
        }

        /// <summary>
        /// Edits the product.
        /// </summary>
        /// <param name="product"> Product. </param>
        /// <returns> Returns the edit action result. </returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product)
        {
            //if the model state is useful
            //i.e the user entered valid parameters
            if (this.ModelState.IsValid)
            {
                //constructing http client
                var httpClient = new HttpClient();

                //constructing base address URI
                httpClient.BaseAddress = this.uri;

                //adding JSON support
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                //serializing the product by making it JSON product
                var json = JsonConvert.SerializeObject(product);

                //sending PUT request and getting the response
                var response = httpClient.PutAsync("api/product",
                    new StringContent(json, Encoding.UTF8, "application/json")).Result;

                //redirecting to showing the list of proucts
                return RedirectToAction("Show");
            }

            //return the same view ,if user entered invalid input
            return View(product);
        }
    }
}