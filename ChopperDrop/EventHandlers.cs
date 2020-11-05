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

namespace ChopperDrop
{
    public class EventHandlers
    {
        public Plugin<Config> pl;

        public int time;
        public string dropText;
        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public bool roundStarted = false;
        public Dictionary<ItemType, int> allowedItems;
        public EventHandlers(Plugin<Config> plugin, Dictionary<ItemType,int> drops, int tim, string dropTex) 
        { 
            pl = plugin;
            allowedItems = drops;
            time = tim;
            dropText = dropTex;
        }

        internal void RoundStart()
        {
            roundStarted = true;
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            Log.Info("Starting Chopper Thread.");
            coroutines.Add(Timing.RunCoroutine(ChopperThread(), "ChopperThread"));
        }

        internal void WaitingForPlayers()
        {
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
        }

        public IEnumerator<float> ChopperThread()
        {
            while(roundStarted)
            {
                yield return Timing.WaitForSeconds(time); // Wait seconds (10 minutes by defualt)
                Log.Info("Spawning chopper!");
                
                RespawnEffectsController.ExecuteAllEffects(RespawnEffectsController.EffectType.Selection, SpawnableTeamType.NineTailedFox);

                Map.Broadcast(5,dropText);

                yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                Vector3 spawn = GetRandomSpawnPoint(RoleType.NtfCadet);

                foreach (KeyValuePair<ItemType, int> drop in allowedItems) // Drop items
                {
                    Log.Info("Spawning " + drop.Value + " " + drop.Key.ToString() + "'s");
                    for (int i = 0; i < drop.Value; i++)
                        SpawnItem(drop.Key, spawn);
                }
                yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the chopper leave.
            }
        }

        public static Vector3 GetRandomSpawnPoint(RoleType roleType)
        {
            GameObject randomPosition = Object.FindObjectOfType<SpawnpointManager>().GetRandomPosition(roleType);

            return randomPosition == null ? Vector3.zero : randomPosition.transform.position;
        }
        
        public int ItemDur(ItemType weapon)
        {
            switch (weapon)
            {
                case ItemType.GunCOM15:
                    return 12;
                case ItemType.GunE11SR:
                    return 18;
                case ItemType.GunProject90:
                    return 50;
                case ItemType.GunMP7:
                    return 35;
                case ItemType.GunLogicer:
                    return 100;
                case ItemType.GunUSP:
                    return 18;
                case ItemType.Ammo762:
                    return 25;
                case ItemType.Ammo9mm:
                    return 25;
                case ItemType.Ammo556:
                    return 25;
                default:
                    return 50;
            }
        }

        public void SpawnItem(ItemType type, Vector3 pos)
        {
            Exiled.API.Extensions.Item.Spawn(type,ItemDur(type),pos);
        }
    }
}
