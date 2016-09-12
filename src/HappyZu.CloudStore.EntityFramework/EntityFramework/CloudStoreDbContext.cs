using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using Abp.Zero.EntityFramework;
using HappyZu.CloudStore.Authorization.Roles;
using HappyZu.CloudStore.FAQ;
using HappyZu.CloudStore.MultiTenancy;
using HappyZu.CloudStore.Trip;
using HappyZu.CloudStore.Users;

namespace HappyZu.CloudStore.EntityFramework
{
    public class CloudStoreDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...

        #region FAQ
        public virtual IDbSet<FAQCategory> FAQCategories { get; set; }

        public virtual IDbSet<FAQDetail> FAQDetails { get; set; }
        #endregion

        #region Trip
        public virtual IDbSet<Dest> Dests { get; set; }
        public virtual IDbSet<DestCity> DestCities { get; set; }
        public virtual IDbSet<DestProvince> DestProvinces { get; set; }
        public virtual IDbSet<Ticket> Tickets { get; set; }
        public virtual IDbSet<TicketCollectingPerson> TicketCollectingPersons { get; set; }
        public virtual IDbSet<TicketOrder> TicketOrders { get; set; }
        public virtual IDbSet<TicketQuotes> TicketQuotes { get; set; }
        public virtual IDbSet<TicketType> TicketTypes { get; set; }
        public virtual IDbSet<Travel> Travels { get; set; }
        public virtual IDbSet<Traveler> Travelers { get; set; }
        public virtual IDbSet<TravelOrder> TravelOrders { get; set; }
        public virtual IDbSet<TravelQuotes> TravelQuotes { get; set; }

        #endregion
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
