using System.Data.Common;
using Abp.Zero.EntityFramework;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.EntityFramework
{
    public class CloudStoreDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public CloudStoreDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in CloudStoreDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of CloudStoreDbContext since ABP automatically handles it.
         */
        public CloudStoreDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public CloudStoreDbContext(DbConnection connection)
            : base(connection, true)
        {

        }
    }
}
