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
        
        public EventHandlers EventHandlers;

        public override void OnDisabled()
        {
            foreach (CoroutineHandle handle in EventHandlers.coroutines)
                Timing.KillCoroutines(handle);
            
            Handlers.Server.RoundStarted -= EventHandlers.RoundStart;
            Handlers.Server.WaitingForPlayers -= EventHandlers.WaitingForPlayers;

            EventHandlers = null;
        }

        public override void OnEnabled()
        {
            if (!Config.IsEnabled) // Enable config
                return;

            EventHandlers = new EventHandlers(this, Config.ChopperItems, Config.ChopperTime, Config.ChopperText, Config.MinPlayers);
            Handlers.Server.RoundStarted += EventHandlers.RoundStart;
            Handlers.Server.WaitingForPlayers += EventHandlers.WaitingForPlayers;

            Log.Info("Chopper Drop enabled! Enjoy :D");
        }

        public override void OnReloaded()
        {
            // empty
        }
    }
}
