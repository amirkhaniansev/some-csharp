using System.Collections.Generic;

namespace BookstoreService
{
    /// <summary>
    /// Class for operation info.
    /// </summary>
    public class OperationInfo
    {
        /// <summary>
        /// Server Name
        /// </summary>
        public string SQL_ServerName { get; set; }

        /// <summary>
        /// Database Name
        /// </summary>
        public string DatabaseName { get; set; }

        /// <summary>
        /// Operation Name
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Operation Type.
        /// </summary>
        public string OperationType { get; set; }

        /// <summary>
        /// Operation Code.
        /// </summary>
        public string OperationCode { get; set; }

        /// <summary>
        /// List of parameters of operation.
        /// </summary>
        public List<string> ParametersList { get; set; }
     }
}