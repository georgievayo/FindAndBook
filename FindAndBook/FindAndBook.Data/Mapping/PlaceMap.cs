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
        }
    }
}
