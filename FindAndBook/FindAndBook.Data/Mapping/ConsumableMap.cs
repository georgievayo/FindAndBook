using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class ConsumableMap : EntityTypeConfiguration<Consumable>
    {
        public ConsumableMap()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Name)
                .IsRequired();

            this.Property(t => t.Type)
                .IsRequired();

            this.HasOptional(t => t.Place);
            
            this.ToTable("Menus");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Type).HasColumnName("Type");
            this.Property(t => t.Price).HasColumnName("Price");
            this.Property(t => t.Quantity).HasColumnName("Quantity");
            this.Property(t => t.Ingredients).HasColumnName("Ingredients");
        }
    }
}
