using EXILED;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChopperDrop
{
    public class ChopperDrops
    {
        public Dictionary<ItemType, int> drops = new Dictionary<ItemType, int>();

        public void AddToList(string item, int amount)
        {
            try
            {
                drops.Add((ItemType)Enum.Parse(typeof(ItemType), item), amount);
            }
            catch
            {
                Plugin.Error("Failed adding item, " + item + " with the amount of " + amount + ". Does it exist?");
            }
        }

    }
}
