using System.Runtime.Serialization;

namespace BookstoreService
{
    /// <summary>
    /// Book structure
    /// </summary>
    [DataContract]
    public struct Book
    {
        /// <summary>
        /// Unique ID for the book.
        /// </summary>
        [DataMember]
        public int ID { get; set; }

        /// <summary>
        /// Author of the book.
        /// </summary>
        [DataMember]
        public string Author { get; set; }

        /// <summary>
        /// Titel of the book.
        /// </summary>
        [DataMember]
        public string Title { get; set; }

        /// <summary>
        /// Price of the book.
        /// </summary>
        [DataMember]
        public double Price { get; set; }

        /// <summary>
        /// Year of the publication.
        /// </summary>
        [DataMember]
        public int Year { get; set; }
    }
}