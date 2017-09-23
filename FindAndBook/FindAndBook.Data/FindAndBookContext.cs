using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FindAndBook.Data.Mapping;
using FindAndBook.Data.Models.Mapping;
using FindAndBook.Models;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FindAndBook.Data
{
    public partial class FindAndBookContext : IdentityDbContext<User>
    {
        public FindAndBookContext() 
            : base("FindAndBook", throwIfV1Schema: false)
        {
            Database.SetInitializer<FindAndBookContext>(null);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<BookedTable> BookedTables { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new BookedTableMap());
            modelBuilder.Configurations.Add(new BookingMap());
            modelBuilder.Configurations.Add(new MenuMap());
            modelBuilder.Configurations.Add(new OrderMap());
            modelBuilder.Configurations.Add(new PlaceMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new TableMap());
        }
    }
}
