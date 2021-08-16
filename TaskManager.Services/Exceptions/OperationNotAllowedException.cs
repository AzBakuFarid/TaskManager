using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Exceptions
{
    public class OperationNotAllowedException : Exception
    {
        public OperationNotAllowedException() : this("The operation is not allowed")
        {
           
        }
        public OperationNotAllowedException(string message) : base(message)
        {
        }
    }
}
