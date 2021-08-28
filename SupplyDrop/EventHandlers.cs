using System;
using MEC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Exiled.API.Features;
using Respawning;
using Map = Exiled.API.Features.Map;
using Object = UnityEngine.Object;
using Exiled.API.Features.Items;
using SupplyDrop.Structs;

namespace SupplyDrop
{
    public class EventHandlers
    {
        public Plugin<Config> pl;

        public int minPlayers;

        public int chopper_time;
        public ushort chopper_bcTime;
        public string chopper_dropText;
        private int chopper_dropLimit;
        private int chopper_dropsNumber = 0;
        private bool chopper_manual_cords;
        private float chopper_posX;
        private float chopper_posY;
        private float chopper_posZ;

        public int car_time;
        public int car_time_difference;
        public ushort car_bcTime;
        public string car_dropText;
        private int car_dropLimit;
        private int car_dropsNumber = 0;
        private bool car_manual_cords;
        private float car_posX;
        private float car_posY;
        private float car_posZ;

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public bool roundStarted = false;

        //public List<DropItems> DropItems { get; set; } = new List<DropItems>();

        public EventHandlers(Plugin<Config> plugin, int minPly, int chopper_tim, string chopper_dropTex, ushort chopper_bcTimee, int chopper_dropslimit, bool chopper_cords_enabled, float chopper_pos_x, float chopper_pos_y, float chopper_pos_z, int car_tim, int car_tim_difference, string car_dropTex, ushort car_bcTimee, int car_dropslimit, bool car_cords_enabled, float car_pos_x, float car_pos_y, float car_pos_z)
        {
            pl = plugin;
            minPlayers = minPly;

            chopper_time = chopper_tim;
            chopper_dropText = chopper_dropTex;
            chopper_bcTime = chopper_bcTimee;
            chopper_dropLimit = chopper_dropslimit;
            chopper_manual_cords = chopper_cords_enabled;
            chopper_posX = chopper_pos_x;
            chopper_posY = chopper_pos_y;
            chopper_posZ = chopper_pos_z;

            car_time = car_tim;
            car_time_difference = car_tim_difference;
            car_dropText = car_dropTex;
            car_bcTime = car_bcTimee;
            car_dropLimit = car_dropslimit;
            car_manual_cords = car_cords_enabled;
            car_posX = car_pos_x;
            car_posY = car_pos_y;
            car_posZ = car_pos_z;
        }

        internal void RoundStart()
        {
            roundStarted = true;
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            Log.Info("Starting Chopper Thread.");
            coroutines.Add(Timing.RunCoroutine(ChopperThread(), "ChopperThread"));
            Log.Info("Starting Car Thread.");
            coroutines.Add(Timing.RunCoroutine(CarThread(), "CarThread"));
        }

        internal void WaitingForPlayers()
        {
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
        }

        public IEnumerator<float> ChopperThread()
        {
            while (roundStarted)
            {
                int playerCount = Player.List.Count(player => player.IsConnected);

                if ((playerCount >= minPlayers) && ((chopper_dropLimit == -1) || (chopper_dropLimit > chopper_dropsNumber)))
                {
                    yield return Timing.WaitForSeconds(chopper_time); // Wait seconds (10 minutes by default)
                    Log.Info("Spawning chopper!");

                    RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);

                    Map.Broadcast(chopper_bcTime, chopper_dropText);

                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                    Vector3 spawn = GetRandomSpawnPoint(RoleType.NtfPrivate);

                    if (chopper_manual_cords) {
                        spawn = new Vector3(chopper_posX, chopper_posY, chopper_posZ);
                    }

                    try
                    {
                        if (SupplyDrop.Singleton.Config.MtfItems == null)
                        {
                            Log.Warn("Null encountered");
                            break;
                        }

                        //Thanks to JesusQC for his help with making the entire plugin work. Love you
                        //Honorable mention - sanyae2439 for "hell code".
                        foreach (var dropItems in SupplyDrop.Singleton.Config.MtfItems) {
                            int spawned = 0;
                            System.Random random = new System.Random();
                            //Has to be declared twice, why? I don't know
                            int r = random.Next(100);
                            Log.Debug($"Preparing to spawn {dropItems.Quantity} {dropItems.Item}(s) with a {dropItems.Chance} chance for each one.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                            for (int i = 0; i < dropItems.Quantity; i++)
                            {
                                r = random.Next(100);
                                if (r <= dropItems.Chance)
                                {
                                    SpawnItem(dropItems.Item, spawn);
                                    spawned++;
                                    Log.Debug($"Spawning {dropItems.Item}. Luck - {r}/{dropItems.Chance}", SupplyDrop.Singleton?.Config?.Debug ?? false);
                                }
                                else Log.Debug($"Item {dropItems.Item} didn't have enought luck.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                            }
                            Log.Debug($"Spawned {spawned}/({dropItems.Quantity} {dropItems.Item}(s)", SupplyDrop.Singleton?.Config?.Debug ?? false);
                        }
                    }
                    catch(Exception e)
                    {
                        Log.Error($"SupplyDrop has encountered a problem while spawning items. Error available below: \n{e} \n--------- End of Error ---------");
                    }

                    chopper_dropsNumber++;
                    Log.Debug($"Drops used - {chopper_dropsNumber}/{chopper_dropLimit}", SupplyDrop.Singleton?.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the chopper leave.
                }
                else {
                    if (chopper_dropsNumber == chopper_dropLimit) Log.Debug("Drops limit has been reached.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(60); // Wait 60 seconds for more players.
                }
            }
        }

        public IEnumerator<float> CarThread()
        {
            while (roundStarted)
            {
                yield return Timing.WaitForSeconds(car_time_difference);

                int playerCount = Player.List.Count(player => player.IsConnected);

                if ((playerCount >= minPlayers) && ((car_dropLimit == -1) || (car_dropLimit > car_dropsNumber)))
                {
                    yield return Timing.WaitForSeconds(car_time); // Wait seconds (10 minutes by default)
                    Log.Info("Spawning car!");

                    RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.ChaosInsurgency);

                    Map.Broadcast(car_bcTime, car_dropText);

                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                    Vector3 spawn = GetRandomSpawnPoint(RoleType.ChaosRifleman);

                    if (car_manual_cords)
                    {
                        spawn = new Vector3(car_posX, car_posY, car_posZ);
                    }

                    try
                    {
                        if (SupplyDrop.Singleton.Config.MtfItems == null)
                        {
                            Log.Warn("Null encountered");
                            break;
                        }

                        //Thanks to JesusQC for his help with making the entire plugin work. Love you
                        //Honorable mention - sanyae2439 for "hell code".
                        foreach (var dropItems in SupplyDrop.Singleton.Config.ChaosItems)
                        {
                            int spawned = 0;
                            System.Random random = new System.Random();
                            //Has to be declared twice, why? I don't know
                            int r = random.Next(100);
                            Log.Debug($"Preparing to spawn {dropItems.Quantity} {dropItems.Item}(s) with a {dropItems.Chance} chance for each one.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                            for (int i = 0; i < dropItems.Quantity; i++)
                            {
                                r = random.Next(100);
                                if (r <= dropItems.Chance)
                                {
                                    SpawnItem(dropItems.Item, spawn);
                                    spawned++;
                                    Log.Debug($"Spawning {dropItems.Item}. Luck - {r}/{dropItems.Chance}", SupplyDrop.Singleton?.Config?.Debug ?? false);
                                }
                                else Log.Debug($"Item {dropItems.Item} didn't have enought luck.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                            }
                            Log.Debug($"Spawned {spawned}/({dropItems.Quantity} {dropItems.Item}(s)", SupplyDrop.Singleton?.Config?.Debug ?? false);
                        }
                    }
                    catch (Exception e)
                    {
                        Log.Error($"SupplyDrop has encountered a problem while spawning items. Error available below: \n{e} \n--------- End of Error ---------");
                    }

                    car_dropsNumber++;
                    Log.Debug($"Drops used - {car_dropsNumber}/{car_dropLimit}", SupplyDrop.Singleton?.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the car leave.
                }
                else
                {
                    if (car_dropsNumber == car_dropLimit) Log.Debug("Drops limit has been reached.", SupplyDrop.Singleton?.Config?.Debug ?? false);
                    yield return Timing.WaitForSeconds(60); // Wait 60 seconds for more players.
                }
            }
        }

        public static Vector3 GetRandomSpawnPoint(RoleType roleType)
        {
            GameObject randomPosition = Object.FindObjectOfType<SpawnpointManager>().GetRandomPosition(roleType);

            return randomPosition == null ? Vector3.zero : randomPosition.transform.position;
        }

        public static void SpawnItem(ItemType type, Vector3 pos)
        {
            Item item = new Item(type);
            item.Spawn(pos, default);
        }
    }
}
