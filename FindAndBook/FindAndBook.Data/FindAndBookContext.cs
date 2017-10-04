using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using FindAndBook.Data.Mapping;
using FindAndBook.Data.Mapping;
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

        public static FindAndBookContext Create()
        {
            return new FindAndBookContext();
        }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Consumable> Consumables { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Table> Tables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AddressMap());
            modelBuilder.Configurations.Add(new BookingMap());
            modelBuilder.Configurations.Add(new ConsumableMap());
            modelBuilder.Configurations.Add(new PlaceMap());
            modelBuilder.Configurations.Add(new ReviewMap());
            modelBuilder.Configurations.Add(new TableMap());
            modelBuilder.Configurations.Add(new UserMap());
            base.OnModelCreating(modelBuilder);
        }

        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SetAdded<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void SetDeleted<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void SetUpdated<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}
