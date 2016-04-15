using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace NorthwindJson.Data
{
    [DbConfigurationType(typeof(DbConfig))]
    public partial class NorthwindSlim : DbContext
    {
        static NorthwindSlim()
        {
            Database.SetInitializer(new NullDatabaseInitializer<NorthwindSlim>());
        }

        public NorthwindSlim(string connectionName) :
            base(connectionName) { }

        public virtual DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Customer>()
                .Property(e => e.CustomerId)
                .IsFixedLength();
        }
    }
}
