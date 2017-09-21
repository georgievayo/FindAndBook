using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FindAndBook.Data.Models.Mapping;

namespace FindAndBook.Data.Models
{
    public partial class FindAndBookContext : DbContext
    {
        static FindAndBookContext()
        {
            Database.SetInitializer<FindAndBookContext>(null);
        }

        public FindAndBookContext()
            : base("Name=FindAndBookContext")
        {
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
