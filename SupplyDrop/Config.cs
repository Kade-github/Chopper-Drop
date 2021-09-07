using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using SupplyDrop.ConfigObjects;
using Exiled.API.Extensions;
using UnityEngine;

namespace SupplyDrop
{
    public class Config : IConfig
    {
        [Description("Please take time to read the Github Readme.")]
        public bool IsEnabled { get; set; } = true;
        [Description("Minimum players on the server to spawn the drops.")]
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
        public int ChopperTime { get; set; } = 600;
        public string ChopperBroadcast { get; set; } = "<size=35><i><color=#0080FF>MTF Chopper</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>";
        public ushort ChopperBroadcastTime { get; set; } = 10;
        [Description("How many drops can the helicopter do per round? Set to -1 to disable limit.")]
        public int ChopperDropsLimit { get; set; } = -1;
        [Description("Should the plugin use coordinates set below to spawn the items? If not it will use random MTF spawn point")]
        public bool ChopperManualCoordinates { get; set; } = true;
        [Description("Coordinates used for the items spawn. Set to 0's to disable")]
        public Vector3 ChopperPosAmmo { get; set; } = new Vector3(173f, 993f, -56f);
        public Vector3 ChopperPosArmors { get; set; } = new Vector3(173f, 993f, -58f);
        public Vector3 ChopperPosItems { get; set; } = new Vector3(173f, 993f, -60f);
        public Vector3 ChopperPosWeapons { get; set; } = new Vector3(173f, 993f, -62f);

        [Description("Settings for Chaos Car Drop")]
        public int CarTime { get; set; } = 600;
        [Description("Time difference between the chopper and car. Chopper will always spawn first. Leave at 1 if you want to disable it.")]
        public int TimeDifference { get; set; } = 300;
        public string CarBroadcast { get; set; } = "<size=35><i><color=#5c5c5c>A</color> <color=#28AD00>Chaos Insurgency Car</color> <color=#5c5c5c>with a</color> <color=#7a7a7a>Supply Drop</color> <color=#5c5c5c>has arrived!</color></i></size>";
        public ushort CarBroadcastTime { get; set; } = 10;
        [Description("How many drops can the car do per round? Set to -1 to disable limit.")]
        public int CarDropsLimit { get; set; } = -1;
        [Description("Should the plugin use coordinates set below to spawn the items? If not it will use random CI spawn point")]
        public bool CarManualCoordinates { get; set; } = true;
        [Description("Coordinates used for the items spawn")]
        public Vector3 CarPosAmmo { get; set; } = new Vector3(2f, 989f, -50f);
        public Vector3 CarPosArmors { get; set; } = new Vector3(4.5f, 989f, -50f);
        public Vector3 CarPosItems { get; set; } = new Vector3(7f, 989f, -50f);
        public Vector3 CarPosWeapons { get; set; } = new Vector3(9.5f, 989f, -50f);
        [Description("Don't use it unless you have issues with the plugin. When sending a log enable this please.")]
        public bool Debug { get; set; } = false;
    }
}