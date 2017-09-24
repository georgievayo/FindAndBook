using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class BookedTableMap : EntityTypeConfiguration<BookedTable>
    {
        public BookedTableMap()
        {
            // Primary Key
            this.HasKey(t => new { t.BookingId, t.TableId });

            // Properties
            this.Property(t => t.BookingId)
                .IsRequired();

            this.Property(t => t.TableId)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("BookedTables");
            this.Property(t => t.BookingId).HasColumnName("BookingId");
            this.Property(t => t.TableId).HasColumnName("TableId");
        }
    }
}
