using FindAndBook.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;

namespace FindAndBook.Data.Mapping
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties

            this.Property(t => t.Country)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.City)
                .IsRequired()
                .HasMaxLength(20);

            this.Property(t => t.Area)
                .HasMaxLength(20);

            this.Property(t => t.Street)
                .IsRequired()
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Addresses");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.HasRequired(t => t.Place);
            this.Property(t => t.Country).HasColumnName("Country");
            this.Property(t => t.City).HasColumnName("City");
            this.Property(t => t.Area).HasColumnName("Area");
            this.Property(t => t.Street).HasColumnName("Street");
            this.Property(t => t.Number).HasColumnName("Number");
        }
    }
}
