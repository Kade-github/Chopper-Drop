using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChopperDrop.Structs
{
    public struct DropItem
    {
        public ItemType Item { get; set; }
        public int Quantity { get; set; }
        public int Chance { get; set; }

        public void Deconstruct(out ItemType name, out int quant, out int number)
        {
            name = Item;
            quant = Quantity;
            number = Chance;
        }
    }
}
