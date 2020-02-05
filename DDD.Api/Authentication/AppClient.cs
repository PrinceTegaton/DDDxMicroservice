using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Api.Authentication
{
    public class AppClient
    {
        public string Name { get; set; }
        public string Key { get; set; }
        public bool IsActive { get; set; }

        public static IEnumerable<AppClient> LoadClients(IEnumerable<AppClient> clients)
        {
            return clients;
        }

        public static IEnumerable<AppClient> GetClients()
        {
            return new List<AppClient>
            {
                new AppClient
                {
                    Name = "GT_BANK",
                    Key = "123456",
                    IsActive = true
                },
                new AppClient
                {
                    Name = "ACCESS_BANK",
                    Key = "654321",
                    IsActive = false
                }
            };
        }
    }
}
