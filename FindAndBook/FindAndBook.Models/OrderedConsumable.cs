using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindAndBook.Models
{
    public class OrderedConsumable
    {
        public Guid? OrderId { get; set; }

        public virtual Order Order { get; set; }

        public Guid? ConsumableId { get; set; }

        public virtual Consumable Consumable { get; set; }
    }
}
