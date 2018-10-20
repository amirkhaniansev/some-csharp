using System.ServiceModel;
using System.Collections.Generic;

namespace BookstoreService
{
    /// <summary>
    /// Interface of BookstoreService
    /// </summary>
    [ServiceContract]
    public interface IBookstoreService
    {
        [OperationContract]
        Result AddBook(string bookAddProtocolText);

        [OperationContract]
        Result UpdateBookPrice(string priceUpdateProtocolText);

        [OperationContract]
        List<Book> GetBooks(out Result result);

    }
}
