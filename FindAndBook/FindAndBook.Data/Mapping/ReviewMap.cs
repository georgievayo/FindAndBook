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
            this.HasKey(t => t.Id);
            this.Property(t => t.Message)
                .IsRequired();

            this.ToTable("Reviews");
            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("Id");
            this.Property(t => t.UserId).HasColumnName("UserId");
            this.Property(t => t.PlaceId).HasColumnName("PlaceId");
            this.Property(t => t.Message).HasColumnName("Message");
            this.Property(t => t.Rating).HasColumnName("Rating");

            this.HasOptional(t => t.User).WithMany().HasForeignKey(t=>t.UserId);
            this.HasOptional(t => t.Place);
        }
    }
}
