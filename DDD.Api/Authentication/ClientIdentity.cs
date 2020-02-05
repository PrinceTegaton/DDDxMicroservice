using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DDD.Api.Authentication
{
    public class ClientIdentity : ClaimsPrincipal
    {
        public ClientIdentity(ClaimsPrincipal principal) : base(principal)
        {

        }

        protected string GetObjectValue(string obj)
        {
            if (string.IsNullOrEmpty(obj)) return string.Empty;
            return obj;
        }

        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId
        {
            get
            {
                return GetObjectValue(this.FindFirst(ClaimTypes.PrimarySid)?.Value);
            }
        }

        /// <summary>
        /// Client Name
        /// </summary>
        public string Name
        {
            get
            {
                return GetObjectValue(this.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }

        }
    }
}
