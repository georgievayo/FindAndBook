using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace FindAndBook.Data.Models.Mapping
{
    public class ReviewMap : EntityTypeConfiguration<Review>
    {
        public ReviewMap()
        {
            // Primary Key
            this.HasKey(t => new { t.PlaceId, t.UserId, t.Review1, t.Rating });

            // Properties
            this.Property(t => t.PlaceId)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.UserId)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Review1)
                .IsRequired();

            this.Property(t => t.Rating)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("Reviews");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.Review1).HasColumnName("Review");
            this.Property(t => t.Rating).HasColumnName("Rating");
        }
    }
}
