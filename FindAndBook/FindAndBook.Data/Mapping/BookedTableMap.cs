using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class BookedTableMap : EntityTypeConfiguration<BookedTable>
    {
        public BookedTableMap()
        {
            this.HasKey(t => new { t.BookingId, t.TableId});

            this.ToTable("BookedTables");
            this.Property(t => t.BookingId).HasColumnName("BookingId");
            this.Property(t => t.TableId).HasColumnName("TableId");

            this.HasRequired(t => t.Booking)
                .WithMany(t => t.Tables)
                .HasForeignKey(d => d.BookingId);

            this.HasRequired(t => t.Table)
                .WithMany(t => t.Bookings)
                .HasForeignKey(d => d.TableId);
        }
    }
}
