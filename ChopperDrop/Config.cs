using System.Collections.Generic;
using Exiled.API.Interfaces;

namespace ChopperDrop
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public Dictionary<ItemType,int> ChopperItems { get; set; } = new Dictionary<ItemType, int> { {ItemType.GunE11SR, 1}, {ItemType.Medkit, 3}, {ItemType.Adrenaline, 2}, {ItemType.Ammo762, 2}};
        public int ChopperTime { get; set; } = 600;
        public string ChopperText { get; set; } = "<color=lime>A supply drop is at the surface!</color>";
        [Description("Minimum players on the server to spawn the chopper")]
        public int MinPlayers { get; set; } = 2;
    }
}
