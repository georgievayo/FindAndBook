using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Models.Mapping
{
    public class TableMap : EntityTypeConfiguration<Table>
    {
        public TableMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PlaceId, t.NumberOfPeople, t.NumberOfTables });

            // Properties
            this.Property(t => t.PlaceId)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.NumberOfPeople)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.NumberOfTables)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Tables");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.NumberOfPeople).HasColumnName("NumberOfPeople");
            this.Property(t => t.NumberOfTables).HasColumnName("NumberOfTables");
        }
    }
}
