using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class BookingMap : EntityTypeConfiguration<Booking>
    {
        public BookingMap()
        {
            // Primary Key
            this.HasKey(t => new { t.Id });

            // Properties
            this.Property(t => t.Id)
                .IsRequired();

            this.Property(t => t.DateTime)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Bookings");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.DateTime).HasColumnName("DateTime");
            this.Property(t => t.NumberOfPeople).HasColumnName("NumberOfPeople");

            this.HasOptional(t => t.Place);
            this.HasOptional(t => t.User).WithMany().HasForeignKey(t => t.UserId);
        }
    }
}
