using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration;

using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class PlaceMap : EntityTypeConfiguration<Place>
    {
        public PlaceMap()
        {
            this.HasKey(t => t.Id);
            
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(30);

            this.Property(t => t.Contact)
                .HasMaxLength(10);

            this.Property(t => t.WeekendHours)
                .HasMaxLength(20);

            this.Property(t => t.WeekdayHours)
                .HasMaxLength(20);

            this.ToTable("Places");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Contact).HasColumnName("Contact");
            this.Property(t => t.WeekendHours).HasColumnName("WeekendHours");
            this.Property(t => t.WeekdayHours).HasColumnName("WeekdayHours");
            this.Property(t => t.Details).HasColumnName("Details");
            this.Property(t => t.AverageBill).HasColumnName("AverageBill");
            this.Property(t => t.ManagerId).HasColumnName("ManagerId");

            this.HasOptional(t => t.Manager).WithMany().HasForeignKey(t=>t.ManagerId);
            this.HasMany(t => t.Reviews);
        }
    }
}
