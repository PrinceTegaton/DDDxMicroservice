using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Api.Authentication
{
    public class APIKeyStore : IAuthorizationRequirement
    {
        public IReadOnlyList<AppClient> Keys { get; set; } = new List<AppClient>();

        public APIKeyStore(IEnumerable<AppClient> clients)
        {
            Keys = clients?.ToList();
        }
    }
}
