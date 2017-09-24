using FindAndBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PlaceId, t.Country, t.City, t.Street, t.Number });

            // Properties
            this.Property(t => t.PlaceId)
                .IsRequired();

            this.Property(t => t.Country)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Area)
                .HasMaxLength(10);

            this.Property(t => t.Street)
                .IsRequired()
                .HasMaxLength(10);

            this.Property(t => t.Number)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Addresses");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Number).HasColumnName("Number");
        }
    }
}
