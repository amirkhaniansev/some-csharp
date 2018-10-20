using System;

namespace DataAccessLayer
{
    /// <summary>
    /// Exception class for database failed operation.
    /// </summary>
    public class FailedDalOperationException:Exception
    {
        /// <summary>
        /// Creates new instance of FailedDbOperationException
        /// </summary>
        public FailedDalOperationException()
        {
        }

        /// <summary>
        /// Creates new instance of FailedDbOperationException
        /// </summary>
        /// <param name="message"> Message of Exception. </param>
        public FailedDalOperationException(string message) : base(message)
        {
        }

        /// <summary>
        /// Creates new instance of FailedDbOperationException
        /// </summary>
        /// <param name="message"> Message of Exception. </param>
        /// <param name="inner"> Inner exception. </param>
        public FailedDalOperationException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}