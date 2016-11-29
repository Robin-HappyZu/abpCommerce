using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.Domain.Repositories;

namespace HappyZu.CloudStore
{
    public static class RepositoryExtensions
    {
        public static void Insert<TEntity, TPrimaryKey>(this IRepository<TEntity, TPrimaryKey> repository,
           IEnumerable<TEntity> entities)
           where TEntity : class, IEntity<TPrimaryKey>, new()
        {
            var type = Type.GetType("HappyZu.CloudStore.RepositoryHelper, HappyZu.CloudStore.EntityFramework");

            var bulkInsertMethod = type.GetMethod("BulkInsert", BindingFlags.Static | BindingFlags.Public);

            var genericMethod = bulkInsertMethod.MakeGenericMethod(typeof(TEntity), typeof(TPrimaryKey));

            genericMethod.Invoke(null, new object[] { repository, entities });
        }
    }
}
