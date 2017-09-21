using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Models.Mapping
{
    public class PlaceMap : EntityTypeConfiguration<Place>
    {
        public PlaceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id, t.Name });

            // Properties
            this.Property(t => t.Id)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Contact)
                .HasMaxLength(10);

            this.Property(t => t.WeekendHours)
                .HasMaxLength(20);

            this.Property(t => t.WeekdayHours)
                .HasMaxLength(20);

            // Table & Column Mappings
            this.ToTable("Places");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.WeekendHours).HasColumnName("WeekendHours");
            this.Property(t => t.WeekdayHours).HasColumnName("WeekdayHours");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.AverageBill).HasColumnName("AverageBill");
        }
    }
}
