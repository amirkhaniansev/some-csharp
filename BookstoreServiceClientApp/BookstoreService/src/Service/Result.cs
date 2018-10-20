using System.Runtime.Serialization;

namespace BookstoreService
{
    /// <summary>
    /// Result structure for operation
    /// </summary>
    [DataContract]
    public struct Result
    {
        /// <summary>
        /// Operation status.
        /// </summary>
        [DataMember]
        public Status Status { get; set; }

        /// <summary>
        /// Message after doing the operation.
        /// </summary>
        [DataMember]
        public string Message { get; set; }
    }
}