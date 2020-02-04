using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DDD.Infrastructure.Repository
{
    public class ManagerBase<TEntity> : SimpleRepository<TEntity> where TEntity : class
    {
        public readonly ILogger<TEntity> Logger;

        public ManagerBase(DbContext context, ILogger<TEntity> logger) : base(context)
        {
            this.Logger = logger;
        }
    }
}
