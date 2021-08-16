using System;


namespace TaskManager.Services.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(): this("The resource you were looking up is not found")
        {

        }
        public NotFoundException(string message) : base(message)
        {
        }
    }
}
