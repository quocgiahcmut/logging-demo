using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest.Domain.Communication
{
    public class Error
    {
        public string ErrorCode { get; protected set; }
        public string Message { get; protected set; }
        public string Detail { get; protected set; }
        public Error(string errorCode, string message, string detail)
        {
            ErrorCode = errorCode;
            Message = message;
            Detail = detail;
        }
        public static Error BadRequest(string errorType, string detail)
        {
            return new Error($"BadRequest.{errorType}", "Request could not be process because of invalid request.", detail);
        }
    }
}
