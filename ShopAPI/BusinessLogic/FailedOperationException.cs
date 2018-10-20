using System;
using DataAccessLayer;

namespace BusinessLogic
{
    /// <summary>
    /// Class for failed operation exception
    /// </summary>
    public class FailedOperationException:Exception
    {
        /// <summary>
        /// Constructs new instance of FailedOperationException
        /// </summary>
        /// <param name="fdbopx"> Failed database operation exception. </param>
        public FailedOperationException(FailedDalOperationException fdbopx):base("Failed operation",fdbopx)
        {            
        }
    }
}