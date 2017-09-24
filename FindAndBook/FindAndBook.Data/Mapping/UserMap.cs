using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FindAndBook.Models;

namespace FindAndBook.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {

            // Table & Column Mappings
            this.ToTable("AspNetUsers");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.UserName).HasColumnName("Username");
            this.Property(t => t.FirstName).HasColumnName("FirstName");
            this.Property(t => t.LastName).HasColumnName("LastName");
            this.Property(t => t.Role).HasColumnName("Role");
        }
    }
}
