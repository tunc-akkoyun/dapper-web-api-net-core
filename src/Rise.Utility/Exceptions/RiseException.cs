using Rise.Utility.Enums;
using System;

namespace Rise.Utility.Exceptions
{
    public class RiseException : Exception
    {
        public ApiStatusCodes Code { get; }

        public RiseException(string message, ApiStatusCodes code) : base(message)
        {
            Code = code;
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
    }
}
