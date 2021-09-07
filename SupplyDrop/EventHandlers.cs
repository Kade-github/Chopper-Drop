using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.API.Features.Items;
using MEC;
using Respawning;
using UnityEngine;
using Map = Exiled.API.Features.Map;
using Object = UnityEngine.Object;
using ItemExtensions = Exiled.API.Extensions.ItemExtensions;
using SupplyDrop.ConfigObjects;

namespace SupplyDrop
{
    public class EventHandlers
    {
        private readonly SupplyDrop pl;
        public EventHandlers(SupplyDrop plugin) => this.pl = plugin;

        private int minPlayers = SupplyDrop.Singleton.Config.MinPlayers;

        private int chopper_time = SupplyDrop.Singleton.Config.ChopperTime;
        private string chopper_dropText = SupplyDrop.Singleton.Config.ChopperBroadcast;
        private ushort chopper_bcTime = SupplyDrop.Singleton.Config.ChopperBroadcastTime;
        private int chopper_dropLimit = SupplyDrop.Singleton.Config.ChopperDropsLimit;
        private Vector3 chopper_pos_ammo = SupplyDrop.Singleton.Config.ChopperPosAmmo;
        private Vector3 chopper_pos_armors = SupplyDrop.Singleton.Config.ChopperPosArmors;
        private Vector3 chopper_pos_items = SupplyDrop.Singleton.Config.ChopperPosItems;
        private Vector3 chopper_pos_weapons = SupplyDrop.Singleton.Config.ChopperPosWeapons;

        private int car_time = SupplyDrop.Singleton.Config.CarTime;
        private int car_time_difference = SupplyDrop.Singleton.Config.TimeDifference;
        private string car_dropText = SupplyDrop.Singleton.Config.CarBroadcast;
        private ushort car_bcTime = SupplyDrop.Singleton.Config.CarBroadcastTime;
        private int car_dropLimit = SupplyDrop.Singleton.Config.CarDropsLimit;
        private Vector3 car_pos_ammo = SupplyDrop.Singleton.Config.CarPosAmmo;
        private Vector3 car_pos_armors = SupplyDrop.Singleton.Config.CarPosArmors;
        private Vector3 car_pos_items = SupplyDrop.Singleton.Config.CarPosItems;
        private Vector3 car_pos_weapons = SupplyDrop.Singleton.Config.CarPosWeapons;

        private int chopper_dropsNumber = 0;
        private int car_dropsNumber = 0;

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        internal void RoundStart()
        {
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            coroutines.Clear();
            Log.Info("Starting Chopper Coroutine.");
            coroutines.Add(Timing.RunCoroutine(ChopperThread(), "ChopperThread"));
            Log.Info("Starting Car Coroutine.");
            coroutines.Add(Timing.RunCoroutine(CarThread(), "CarThread"));
        }

        internal void WaitingForPlayers()
        {
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            coroutines.Clear();
        }

        public IEnumerator<float> ChopperThread()
        {
            while (Round.IsStarted)
            {
                if ((Server.PlayerCount >= minPlayers) && ((chopper_dropLimit == -1) || (chopper_dropLimit > chopper_dropsNumber)))
                {
                    yield return Timing.WaitForSeconds(chopper_time); // Wait seconds (10 minutes by default)
                    Log.Info("Spawning chopper!");

                    RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);

                    Map.Broadcast(chopper_bcTime, chopper_dropText);

                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                    Vector3 spawn = Exiled.API.Extensions.RoleExtensions.GetRandomSpawnProperties(RoleType.NtfPrivate).Item1;

                    try
                    {
                        if (SupplyDrop.Singleton.Config.MtfItems == null)
                        {
                            Log.Warn("MtfItems config is null. Check your config for any errors.");
                            break;
                        }

                        //Thanks to JesusQC for his help with making the entire plugin work. Love you
                        //Honorable mention - sanyae2439 for "hell code".
                        System.Random random = Exiled.Loader.Loader.Random;
                        foreach (var dropItems in SupplyDrop.Singleton.Config.MtfItems) {
                            int spawned = 0;
                            Item item = new Item(dropItems.Item);
                            if (ItemExtensions.IsAmmo(item.Type) && (chopper_pos_ammo != Vector3.zero)) spawn = chopper_pos_ammo;
                            if (ItemExtensions.IsArmor(item.Type) && (chopper_pos_armors != Vector3.zero)) spawn = chopper_pos_armors;
                            if (ItemExtensions.IsKeycard(item.Type) || ItemExtensions.IsMedical(item.Type) || ItemExtensions.IsUtility(item.Type) || ItemExtensions.IsScp(item.Type) && (chopper_pos_items != Vector3.zero)) spawn = chopper_pos_items;
                            if (ItemExtensions.IsWeapon(item.Type, true) || ItemExtensions.IsThrowable(item.Type) && (chopper_pos_weapons != Vector3.zero)) spawn = chopper_pos_weapons;
                            Log.Debug($"Coordinates choosed for {dropItems.Item} - {spawn}", pl.Config?.Debug ?? false);
                            int r = random.Next(100);
                            Log.Debug($"Preparing to spawn {dropItems.Quantity} {dropItems.Item}(s) with a {dropItems.Chance} chance for each one.", pl.Config?.Debug ?? false);
                            for (int i = 0; i < dropItems.Quantity; i++)
                            {
                                r = random.Next(100);
                                if (r <= dropItems.Chance)
                                {
                                    item.Spawn(spawn, default);
                                    spawned++;
                                    Log.Debug($"Spawning {dropItems.Item}. Luck - {r}/{dropItems.Chance}", pl.Config?.Debug ?? false);
                                }
                                else Log.Debug($"Item {dropItems.Item} didn't have enought luck.", pl.Config?.Debug ?? false);
                            }
                            Log.Debug($"Spawned {spawned}/({dropItems.Quantity} {dropItems.Item}(s)", pl.Config?.Debug ?? false);
                        }
                    }
                    catch(Exception e)
                    {
                        Log.Error($"SupplyDrop has encountered a problem while spawning items. Error available below: \n{e} \n--------- End of Error ---------");
                    }

                    chopper_dropsNumber++;
                    Log.Debug($"Drops used - {chopper_dropsNumber}/{chopper_dropLimit}", pl.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the chopper leave.
                }
                else {
                    if (chopper_dropsNumber == chopper_dropLimit) Log.Debug("Drops limit has been reached.", pl.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(60); // Wait 60 seconds for more players.
                }
            }
        }

        public IEnumerator<float> CarThread()
        {
            while (Round.IsStarted)
            {
                yield return Timing.WaitForSeconds(car_time_difference);

                if ((Server.PlayerCount >= minPlayers) && ((car_dropLimit == -1) || (car_dropLimit > car_dropsNumber)))
                {
                    yield return Timing.WaitForSeconds(car_time); // Wait seconds (10 minutes by default)
                    Log.Info("Spawning car!");

                    RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.ChaosInsurgency);

                    Map.Broadcast(car_bcTime, car_dropText);

                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                    Vector3 spawn = Exiled.API.Extensions.RoleExtensions.GetRandomSpawnProperties(RoleType.ChaosRifleman).Item1;

                    try
                    {
                        if (SupplyDrop.Singleton.Config.ChaosItems == null)
                        {
                            Log.Warn("ChaosItems config is null. Check your config for any errors.");
                            break;
                        }

                        //Thanks to JesusQC for his help with making the entire plugin work. Love you
                        //Honorable mention - sanyae2439 for "hell code".
                        System.Random random = Exiled.Loader.Loader.Random;
                        foreach (var dropItems in SupplyDrop.Singleton.Config.ChaosItems)
                        {
                            int spawned = 0;
                            Item item = new Item(dropItems.Item);
                            if (ItemExtensions.IsAmmo(item.Type) && (car_pos_ammo != Vector3.zero)) spawn = car_pos_ammo;
                            if (ItemExtensions.IsArmor(item.Type) && (car_pos_armors != Vector3.zero)) spawn = car_pos_armors;
                            if (ItemExtensions.IsKeycard(item.Type) || ItemExtensions.IsMedical(item.Type) || ItemExtensions.IsUtility(item.Type) || ItemExtensions.IsScp(item.Type) && (car_pos_items != Vector3.zero)) spawn = car_pos_items;
                            if (ItemExtensions.IsWeapon(item.Type, true) || ItemExtensions.IsThrowable(item.Type) && (car_pos_weapons != Vector3.zero)) spawn = car_pos_weapons;
                            Log.Debug($"Coordinates choosed for {dropItems.Item} - {spawn}", pl.Config?.Debug ?? false);
                            int r = random.Next(100);
                            Log.Debug($"Preparing to spawn {dropItems.Quantity} {dropItems.Item}(s) with a {dropItems.Chance} chance for each one.", pl.Config?.Debug ?? false);
                            for (int i = 0; i < dropItems.Quantity; i++)
                            {
                                r = random.Next(100);
                                if (r <= dropItems.Chance)
                                {
                                    item.Spawn(spawn, default);
                                    spawned++;
                                    Log.Debug($"Spawning {dropItems.Item}. Luck - {r}/{dropItems.Chance}", pl.Config?.Debug ?? false);
                                }
                                else Log.Debug($"Item {dropItems.Item} didn't have enought luck.", pl.Config?.Debug ?? false);
                            }
                            Log.Debug($"Spawned {spawned}/({dropItems.Quantity} {dropItems.Item}(s)", pl.Config?.Debug ?? false);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error($"SupplyDrop has encountered a problem while spawning items. Error available below: \n{e} \n--------- End of Error ---------");
                    }

                    car_dropsNumber++;
                    Log.Debug($"Drops used - {car_dropsNumber}/{car_dropLimit}", pl.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the car leave.
                }
                else
                {
                    if (car_dropsNumber == car_dropLimit) Log.Debug("Drops limit has been reached.", pl.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(60); // Wait 60 seconds for more players.
                }
            }
        }
    }
}
