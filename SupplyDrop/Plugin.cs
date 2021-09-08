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

namespace SupplyDrop
{
    public class SupplyDrop : Plugin<Config>
    {
        public override string Author { get; } = "Wafel, KadeDev, JesusQC";
        public override string Name { get; } = "SupplyDrop";
        public override string Prefix { get; } = "SD";
        public override Version Version { get; } = new Version(3, 0, 0);
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        internal static SupplyDrop Singleton;

        public EventHandlers EventHandlers;

        public override void OnEnabled()
        {
            Singleton = this;

            EventHandlers = new EventHandlers(this);
            Handlers.Server.RoundStarted += EventHandlers.RoundStart;
            Handlers.Server.WaitingForPlayers += EventHandlers.WaitingForPlayers;

            Log.Info("Supply Drop enabled! Enjoy :D");
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
    }
}
