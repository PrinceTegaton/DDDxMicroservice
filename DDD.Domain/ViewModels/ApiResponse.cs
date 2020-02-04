using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain
{
    public class ApiResponse
    {
        public ResponseCodes Code { get; set; } = ResponseCodes.Failed;
        public string Message { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }

    public enum ResponseCodes
    {
        Ok = 1,
        InvalidRequest = 2,
        Failed = 3,
        Unauthorized = 4,
        Denied = 5
    }
}
