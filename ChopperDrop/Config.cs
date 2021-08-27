using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using ChopperDrop.Structs;
using Exiled.API.Extensions;

namespace ChopperDrop
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("List of items ")]
        public Dictionary<Exiled.API.Enums.Side, List<DropItem>> ChopperItems { get; set; } = new Dictionary<Exiled.API.Enums.Side, List<DropItem>>
        {
            {
                Exiled.API.Enums.Side.Mtf, new List<DropItem>
                {
                    {
                        new DropItem
                        {
                            Item = ItemType.Coin,
                            Quantity = 5,
                            Chance = 50,
                        }
                    }
                }
            },
        };
        //public Dictionary<ItemType, int> ChopperItems { get; set; } = new Dictionary<ItemType, int> 
        //{ 
        //    { 
        //        ItemType.GunCOM18, 1 
        //    }, { 
        //        ItemType.GunE11SR, 1 
        //    }, { 
        //        ItemType.Ammo762x39, 1 
        //    }, { 
        //        ItemType.Ammo9x19, 1 
        //    }, { 
        //        ItemType.Medkit, 2 
        //    }, { 
        //        ItemType.Adrenaline, 1 
        //    }, { 
        //        ItemType.Coin, 1 
        //    } 
        //};
        public int ChopperTime { get; set; } = 600;
        public string ChopperBroadcast { get; set; } = "<size=45><i><color=yellow>A supply drop has arrived!</color></i></size>";
        public ushort ChopperBroadcastTime { get; set; } = 10;
        [Description("How many drops can the helicopter do per round? Set to -1 to disable limit.")]
        public int DropsLimit { get; set; } = -1;
        [Description("Minimum players on the server to spawn the chopper")]
        public int MinPlayers { get; set; } = 2;
        [Description("Should the plugin use coordinates set below to spawn the items? If not it will use random MTF spawn point")]
        public bool ManualCoordinates { get; set; } = true;
        [Description("Coordinates used for the items spawn")]
        public float Pos_x { get; set; } = 173;
        public float Pos_y { get; set; } = 993;
        public float Pos_z { get; set; } = -59;
        [Description("Don't use it unless you have issues with the plugin. When sending a log enable this please.")]
        public bool Debug { get; set; } = false;
    }
}