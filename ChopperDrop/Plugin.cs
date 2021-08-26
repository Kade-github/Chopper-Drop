using Handlers = Exiled.Events.Handlers;
using MEC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exiled.API.Features;
using Exiled.Events;
using Config = Exiled.Loader.Config;

namespace ChopperDrop
{
    public class ChopperDrop : Plugin<Config>
    {
        public override string Author { get; } = "KadeDev";
        public override string Name { get; } = "Chopper Drop";
        public override string Prefix { get; } = "CD";
        public override Version Version { get; } = new Version(2, 7, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        internal static ChopperDrop Singleton;

        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) // Enable config
                return;

            EventHandlers = new EventHandlers(this, Config.ChopperItems, Config.ChopperTime, Config.ChopperBroadcast, Config.MinPlayers, Config.ChopperBroadcastTime, Config.DropsLimit, Config.ManualCoordinates, Config.Pos_x, Config.Pos_y, Config.Pos_z);
            Handlers.Server.RoundStarted += EventHandlers.RoundStart;
            Handlers.Server.WaitingForPlayers += EventHandlers.WaitingForPlayers;

            Log.Info("Chopper Drop enabled! Enjoy :D");
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            foreach (CoroutineHandle handle in EventHandlers.coroutines)
                Timing.KillCoroutines(handle);
            
            Handlers.Server.RoundStarted -= EventHandlers.RoundStart;
            Handlers.Server.WaitingForPlayers -= EventHandlers.WaitingForPlayers;

            EventHandlers = null;
            base.OnDisabled();
        }

        public override void OnReloaded()
        {
            // empty
        }
    }
}
