using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class PlaceMap : EntityTypeConfiguration<Place>
    {
        public PlaceMap()
        {            
            this.Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.HasOptional(x => x.Address);
            this.HasMany(x => x.Bookings)
                .WithRequired(x => x.Place)          
                .WillCascadeOnDelete();
            this.HasMany(x => x.Reviews)
                .WithRequired(x => x.Place)
                .WillCascadeOnDelete();
            this.HasMany(x => x.Tables)
                .WithRequired(x => x.Place)
                .WillCascadeOnDelete();
            this.HasMany(x => x.Consumables)
                .WithRequired(x => x.Place)
                .WillCascadeOnDelete();
            this.HasRequired(x => x.Manager)
                .WithMany(x => x.Places)
                .WillCascadeOnDelete();
        }
    }
}
