using System;
using MEC;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using EXILED;

namespace ChopperDrop
{
    public class EventHandlers
    {
        public Plugin pl;
        public ChopperDrops allowedItems;

        public int time;

        public List<CoroutineHandle> coroutines = new List<CoroutineHandle>();

        public bool roundStarted = false;

        public EventHandlers(Plugin plugin, ChopperDrops drops, int tim) 
        { 
            pl = plugin;
            allowedItems = drops;
            time = tim;
        }

        internal void RoundStart()
        {
            roundStarted = true;
            foreach (CoroutineHandle handle in coroutines)
                Timing.KillCoroutines(handle);
            Plugin.Info("Starting the ChopperThread.");
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
                // Unity GARBAGE
                Plugin.Info("Chopper thread waiting 10 minutes.");
                yield return Timing.WaitForSeconds(time); // Wait seconds (10 minutes by defualt)
                Plugin.Info("Spawning chopper!");
                ChopperAutostart ca = UnityEngine.Object.FindObjectOfType<ChopperAutostart>(); // Call in the chopper
                ca.SetState(true);

                yield return Timing.WaitForSeconds(15); // Wait 15 seconds

                Vector3 spawn = Plugin.GetRandomSpawnPoint(RoleType.NtfCadet); // Get the spawn point of the chopper to you know, spawn em.

                foreach (KeyValuePair<ItemType, int> drop in allowedItems.drops) // Drop items
                {
                    Plugin.Info("Spawning " + drop.Value + " " + drop.Key.ToString() + "'s");
                    for (int i = 0; i < drop.Value; i++)
                    {
                        SpawnItem(drop.Key, spawn, spawn);
                    }
                }
                ca.SetState(false); // Call the chopper to leave
                yield return Timing.WaitForSeconds(15); // Wait 15 seconds to let the chopper leave.
            }
        }

        public void SpawnItem(ItemType type, Vector3 pos, Vector3 rot)
        {
            PlayerManager.localPlayer.GetComponent<Inventory>().SetPickup(type, -4.656647E+11f, pos, Quaternion.Euler(rot), 0, 0, 0);
        }
    }
}
