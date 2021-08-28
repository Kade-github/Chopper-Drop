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

            if (!Config.IsEnabled) // Enable config
                return;

            EventHandlers = new EventHandlers(this, Config.MinPlayers, Config.Chopper_Time, Config.Chopper_Broadcast, Config.Chopper_BroadcastTime, Config.Chopper_DropsLimit, Config.Chopper_ManualCoordinates, Config.Chopper_Pos_x, Config.Chopper_Pos_y, Config.Chopper_Pos_z, Config.Car_Time, Config.Time_Difference, Config.Car_Broadcast, Config.Car_BroadcastTime, Config.Car_DropsLimit, Config.Car_ManualCoordinates, Config.Car_Pos_x, Config.Car_Pos_y, Config.Car_Pos_z);
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

        public override void OnReloaded()
        {
            // empty
        }
    }
}
