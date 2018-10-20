using System;

namespace BookstoreService
{
    /// <summary>
    /// Class for parsing protocol strings
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse the book addition protocol text to an instance of book.
        /// </summary>
        /// <param name="bookAddProtocolText"> Book's addition protocol text.</param>
        /// <returns> Returns an instance of book corresponding the protocol text.</returns>
        public static Book ParseToBook(string bookAddProtocolText)
        {
            //some validations for protocol text
            if (bookAddProtocolText == null)
            {
                throw  new ArgumentNullException(nameof(bookAddProtocolText));
            }

            var protocol = bookAddProtocolText.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            if (protocol.Length != 5)
            {
                throw new FormatException("Not enough parameters");
            }

            //storing author and title
            var author = protocol[1];
            var title = protocol[2];

            //checking if the paramaters given by the user can be parsed
            if (!(int.TryParse(protocol[0], out var id) &&
                int.TryParse(protocol[4], out var year) &&
                double.TryParse(protocol[3], out var price)))
            {
                throw new FormatException("Unable to parse");
            }

            //if everything is ok,return the instance of book corresponding the protocol text
            return new Book
            {
                ID = id,
                Author = author,
                Title = title,
                Price = price,
                Year = year
            };
        }

        /// <summary>
        /// Parses the price update protocol text to an instance of PriceUpdateInfo.
        /// </summary>
        /// <param name="priceUpdateProtocolText">Price Update Protocol Text.</param>
        /// <returns> Returns an instance of PriceUpdateInfo corresponding the protocol text.</returns>
        public static PriceUpdateInfo ParseToPriceUpdateInfo(string priceUpdateProtocolText)
        {
            //validating the protocol text
            if (priceUpdateProtocolText == null)
            {
                throw new ArgumentNullException(nameof(priceUpdateProtocolText));
            }

            var protocol = priceUpdateProtocolText.Split(new[] {':'}, StringSplitOptions.RemoveEmptyEntries);

            if (protocol.Length != 2)
            {
                throw new FormatException("Not enough parameters");
            }

            if (!(int.TryParse(protocol[0], out var id) && double.TryParse(protocol[1], out var newPrice)))
            {
                throw new FormatException("Unable to parse");
            }

            //return the instance of PriceUpdateInfo if everything is ok.
            return new PriceUpdateInfo
            {
                ID = id,
                NewPrice = newPrice
            };
        }
    }
}