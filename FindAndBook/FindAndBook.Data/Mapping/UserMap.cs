using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasMany(x => x.Bookings)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete();
            this.HasMany(x => x.Reviews)
                .WithRequired(x => x.User)
                .WillCascadeOnDelete();
            this.HasMany(x => x.Places)
                .WithRequired(x => x.Manager)
                .WillCascadeOnDelete();
        }
    }
}
