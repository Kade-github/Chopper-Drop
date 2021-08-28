using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using SupplyDrop.Structs;
using Exiled.API.Extensions;

namespace SupplyDrop
{
    public class Config : IConfig
    {
        [Description("Please take time to read the Github Readme.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Minimum players on the server to spawn the chopper")]
        public int MinPlayers { get; set; } = 2;
        [Description("List of MTF Chopper Drop items")]
        public List<DropItems> MtfItems { get; set; } = new List<DropItems>()
        {
            new DropItems
            {
                Item = ItemType.GunCOM18,
                Quantity = 1,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.GunE11SR,
                Quantity = 1,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Ammo762x39,
                Quantity = 2,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Ammo9x19,
                Quantity = 2,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Medkit,
                Quantity = 2,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Medkit,
                Quantity = 2,
                Chance = 20,
            },
            new DropItems
            {
                Item = ItemType.Adrenaline,
                Quantity = 1,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.KeycardO5,
                Quantity = 1,
                Chance = 10,
            }
        };
        [Description("List of Chaos Car Drop items")]
        public List<DropItems> ChaosItems { get; set; } = new List<DropItems>()
        {
            new DropItems
            {
                Item = ItemType.GunLogicer,
                Quantity = 2,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Ammo762x39,
                Quantity = 5,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.Medkit,
                Quantity = 2,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.ArmorCombat,
                Quantity = 2,
                Chance = 20,
            },
            new DropItems
            {
                Item = ItemType.Adrenaline,
                Quantity = 1,
                Chance = 100,
            },
            new DropItems
            {
                Item = ItemType.KeycardO5,
                Quantity = 1,
                Chance = 10,
            }
        };
        [Description("Settings for MTF Chopper Drop")]
        public int Chopper_Time { get; set; } = 600;
        public string Chopper_Broadcast { get; set; } = "<size=35><i><color=#0080FF>MTF Chopper</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>";
        public ushort Chopper_BroadcastTime { get; set; } = 10;
        [Description("How many drops can the helicopter do per round? Set to -1 to disable limit.")]
        public int Chopper_DropsLimit { get; set; } = -1;
        [Description("Should the plugin use coordinates set below to spawn the items? If not it will use random MTF spawn point")]
        public bool Chopper_ManualCoordinates { get; set; } = true;
        [Description("Coordinates used for the items spawn")]
        public float Chopper_Pos_x { get; set; } = 173;
        public float Chopper_Pos_y { get; set; } = 993;
        public float Chopper_Pos_z { get; set; } = -59;

        [Description("Settings for Chaos Car Drop")]
        public int Car_Time { get; set; } = 600;
        [Description("Time difference between the chopper and car. Chopper will always spawn first. Leave at 1 if you want to disable it.")]
        public int Time_Difference { get; set; } = 300;
        public string Car_Broadcast { get; set; } = "<size=35><i><color=#5c5c5c>A</color> <color=#28AD00>Chaos Insurgency Car</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>";
        public ushort Car_BroadcastTime { get; set; } = 10;
        [Description("How many drops can the car do per round? Set to -1 to disable limit.")]
        public int Car_DropsLimit { get; set; } = -1;
        [Description("Should the plugin use coordinates set below to spawn the items? If not it will use random CI spawn point")]
        public bool Car_ManualCoordinates { get; set; } = true;
        [Description("Coordinates used for the items spawn")]
        public float Car_Pos_x { get; set; } = 9;
        public float Car_Pos_y { get; set; } = 998;
        public float Car_Pos_z { get; set; } = -49;
        [Description("Don't use it unless you have issues with the plugin. When sending a log enable this please.")]
        public bool Debug { get; set; } = false;
    }
}