using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;

namespace ChopperDrop
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        public Dictionary<ItemType,int> ChopperItems { get; set; } = new Dictionary<ItemType, int> { {ItemType.GunCOM18, 1}, {ItemType.GunE11SR, 1}, { ItemType.Ammo762x39, 1 }, { ItemType.Ammo9x19, 1 }, { ItemType.Medkit, 2}, {ItemType.Adrenaline, 1}, { ItemType.Coin, 1 } };
        public int ChopperTime { get; set; } = 600;
        public string ChopperText { get; set; } = "<size=45><i><color=yellow>A supply drop has arrived!</color></i></size>";
        [Description("Minimum players on the server to spawn the chopper")]
        public int MinPlayers { get; set; } = 2;
    }
}