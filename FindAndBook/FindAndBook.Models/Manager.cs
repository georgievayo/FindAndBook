using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndBook.Models
{
    public class Manager : User
    {
        public Manager()
            : base()
        {
            this.Places = new HashSet<Place>();
        }

        public Manager(string username, string email)
            : base(username, email)
        {
            
        }

        public virtual ICollection<Place> Places { get; set; }
    }
}
