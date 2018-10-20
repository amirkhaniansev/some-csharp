using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Script.Serialization;

namespace ConsoleUI
{
    /// <summary>
    /// Class for processing user inputs and outputs
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse product from string.
        /// </summary>
        /// <param name="product"> String representation of product. </param>
        /// <returns> Returns product. </returns>
        public static ClientProduct ParseFromString(string product)
        {
            //splitting product string
            var data = product.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            //if product string has invalid format then throw an exception
            if (data.Length != 4)
            {
                throw new FormatException("Invalid product text");
            }

            //if the values are not parsable,throw an exception
            if (!int.TryParse(data[0], out var id) || !double.TryParse(data[3], out var price))
            {
                throw new FormatException("Unable to parse");
            }

            //name of product
            var name = data[1];

            //category of product
            var category = data[2];

            //returning new Client product
            return new ClientProduct
            {
                ID = id,
                Name = name,
                Category = category,
                Price = price
            };
        }

        /// <summary>
        /// Converts client product to content.
        /// </summary>
        /// <param name="clientProduct"> Client product.</param>
        /// <returns> Returns content. </returns>
        public static FormUrlEncodedContent ConvertToContent(ClientProduct clientProduct)
        {
            //constructing dictionary
            var contentDictionary = new Dictionary<string, string>
            {
                {nameof(clientProduct.ID), clientProduct.ID.ToString()},
                {nameof(clientProduct.Name), clientProduct.Name},
                {nameof(clientProduct.Category), clientProduct.Category},
                {nameof(clientProduct.Price), clientProduct.Price.ToString()}
            };

            //returning content
            return new FormUrlEncodedContent(contentDictionary);
        }

        /// <summary>
        /// Parses the product objects from JSON.
        /// </summary>
        /// <param name="json"> JSON string.</param>
        /// <returns> Returns the enumerable of client products.</returns>
        public static IEnumerable<ClientProduct> ParseFromJson(string json)
        {
            //constructing JS serializer
            var serializer = new JavaScriptSerializer();

            //deserializing JSON
            var products = serializer.Deserialize<IEnumerable<ClientProduct>>(json);
            
            //returning products
            return products;
        }

       
    }
}