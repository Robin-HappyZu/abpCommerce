using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;

namespace HappyZu.CloudStore
{
    public static class RepositoryHelper
    {
        public static void BulkInsert<TEntity, TPrimaryKey>(IRepository<TEntity, TPrimaryKey> repository, IEnumerable<TEntity> entities)
           where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var dbContext = repository.GetDbContext();
            dbContext.Set<TEntity>().AddRange(entities);
        }
    }
}
