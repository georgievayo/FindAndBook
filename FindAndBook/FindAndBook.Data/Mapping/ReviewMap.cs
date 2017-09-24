using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            this.HasKey(t => new {t.PlaceId, t.UserId});
            // Properties
            this.Property(t => t.PlaceId)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired();

            this.Property(t => t.Message)
                .IsRequired();

            this.Property(t => t.Rating)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Reviews");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.Rating).HasColumnName("Rating");
        }
    }
}
