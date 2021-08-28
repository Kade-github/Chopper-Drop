using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupplyDrop.Structs
{
    public class DropItems
    {
        public ItemType Item { get; set; }
        public int Quantity { get; set; }
        public int Chance{ get; set; }
    }
}
