using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Domain
{
    public class ApiResponse
    {
        public ResponseCodes StatusCode { get; set; } = ResponseCodes.Failed;
        public string Status
        {
            get
            {
                return StatusCode.ToString();
            }
        }
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
        Error = 5
    }
}
