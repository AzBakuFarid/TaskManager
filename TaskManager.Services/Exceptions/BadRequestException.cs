using System;


namespace TaskManager.Services.Exceptions
{
    public class BadRequestException : Exception
    {
        public string FieldName { get; }
        public BadRequestException(string fieldname, string message) : base(message)
        {
            FieldName = fieldname;
        }
    }
}
