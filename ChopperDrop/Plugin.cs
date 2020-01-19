using EXILED;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChopperDrop
{
    public class ChopperDrop : EXILED.Plugin
    {
        public EventHandlers EventHandlers;

        public override string getName => "ChopperDrop";

        public override void OnDisable()
        {
            foreach (CoroutineHandle handle in EventHandlers.coroutines)
                Timing.KillCoroutines(handle);

            Events.RoundStartEvent -= EventHandlers.RoundStart;
            Events.WaitingForPlayersEvent -= EventHandlers.WaitingForPlayers;

            EventHandlers = null;
        }

        public override void OnEnable()
        {
            // We make our own dictionary stuff because the .GetStringDictionary of 'config' me and joker don't know how it works lol.
            string[] drops = Config.GetString("chopper_drops", "GrenadeFrag:4,Flashlight:1,GunMP7:4,GunUSP:2,Painkillers:4").Split(',');
            ChopperDrops cDrops = new ChopperDrops();

            int time = Config.GetInt("chopper_time", 600);

            foreach (string drop in drops)
            {
                string[] d = drop.Split(':'); // d[0] = item, d[1] = amount
                cDrops.AddToList(d[0], int.Parse(d[1]));
            }

            EventHandlers = new EventHandlers(this, cDrops, time);
            Events.RoundStartEvent += EventHandlers.RoundStart;
            Events.WaitingForPlayersEvent += EventHandlers.WaitingForPlayers;

            Info("Chopper Drop enabled! Enjoy :D");
        }

        public override void OnReload()
        {
            // empty
        }
    }
}
