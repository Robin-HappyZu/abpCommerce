using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace HappyZu.CloudStore.EntityFramework.Repositories
{
    public abstract class CloudStoreRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<CloudStoreDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected CloudStoreRepositoryBase(IDbContextProvider<CloudStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class CloudStoreRepositoryBase<TEntity> : CloudStoreRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected CloudStoreRepositoryBase(IDbContextProvider<CloudStoreDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
