using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class TableMap : EntityTypeConfiguration<Table>
    {
        public TableMap()
        {
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.NumberOfPeople)
                .IsRequired();

            this.Property(t => t.NumberOfTables)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("Tables");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.NumberOfPeople).HasColumnName("NumberOfPeople");
            this.Property(t => t.NumberOfTables).HasColumnName("NumberOfTables");
        }
    }
}
