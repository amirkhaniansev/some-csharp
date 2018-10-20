using System.Net;
using System.Web.Http;
using System.Collections.Generic;
using BusinessLogic;

namespace WebService.Controllers
{
    /// <summary>
    /// Controller for products
    /// </summary>
    public class ProductController : ApiController
    {
        /// <summary>
        /// Repository for accessing products
        /// </summary>
        private readonly Repository repository;

        /// <summary>
        /// Creates new instance of product controller.
        /// </summary>
        public ProductController()
        {
            //creating repository
            this.repository = new Repository();
        }

        /// <summary>
        /// Gets the products from database.
        /// </summary>
        /// <returns> Returns an enumerable of products. </returns>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            //enumerable of products
            var products = null as IEnumerable<Product>;

            //reading products information form repository
            try
            {
                products = this.repository.Read();
            }

            catch (FailedOperationException)
            {
                //if failed operation exception is catched
                //then throw new HttpResponseException

                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            //returning products
            return products;
        }


        /// <summary>
        /// Performs POST request.
        /// </summary>
        /// <param name="value"> Value. </param>
        [HttpPost]
        public void Post([FromBody]Product value)
        {
            //Posting new product
            try
            {
                this.repository.Create(value);
            }

            catch (FailedOperationException)
            {
                //if an error occured then construct specific HttpResponseMessage and
                //throw HttpResponseException
                throw new HttpResponseException(HttpStatusCode.Conflict);
            }
        }

        /// <summary>
        /// Perfroms PUT request.
        /// </summary>
        /// <param name="value"> Value.</param>
        [HttpPut]
        public void Put([FromBody]Product value)
        {
            //performing PUT request
            try
            {
                this.repository.Update(value);
            }

            catch (FailedOperationException)
            {
                //if an error occured then construct specific HttpResponseMessage and
                //throw HttpResponseException
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Deletes the product with the given ID.
        /// </summary>
        /// <param name="id"> Product ID.</param>
        [HttpDelete]
        public void Delete(int id)
        {
            //Performing DELETE request
            try
            {
                this.repository.Delete(id);
            }
            catch (FailedOperationException)
            {
                //if an error occured then construct specific HttpResponseMessage and
                //throw HttpResponseException
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
