namespace DocRework
{
    using Configs;
    using Exiled.API.Features;
    using Handlers;
    using PlayerEvents = Exiled.Events.Handlers.Player;
    using Scp049Events = Exiled.Events.Handlers.Scp049;
    using ServerEvents = Exiled.Events.Handlers.Server;

    public class DocRework : Plugin<Config>
    {
        internal static DocRework Instance;
        private PlayerHandlers _playerHandlers;
        private Scp049Handlers _scp049Handlers;
        private ServerHandlers _serverHandlers;

        public override void OnEnabled()
        {
            if (Config.DoctorConfigs.HealType > 1)
            {
                Config.DoctorConfigs.HealType = 0;
                Log.Info(
                    "HealType is defaulted to 0 (Flat HP mode) due to incorrect HealType configuration. (HealType cannot be greater than 1).");
            }

            Instance = this;
            _playerHandlers = new PlayerHandlers();
            _scp049Handlers = new Scp049Handlers();
            _serverHandlers = new ServerHandlers();
            PlayerEvents.Hurting += _playerHandlers.OnPlayerHurt;
            Scp049Events.FinishingRecall += _scp049Handlers.OnFinishingRecall;
            ServerEvents.RoundStarted += _serverHandlers.OnRoundStart;
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            PlayerEvents.Hurting -= _playerHandlers.OnPlayerHurt;
            Scp049Events.FinishingRecall -= _scp049Handlers.OnFinishingRecall;
            ServerEvents.RoundStarted -= _serverHandlers.OnRoundStart;
            _playerHandlers = null;
            _scp049Handlers = null;
            _serverHandlers = null;
            Instance = null;
            base.OnDisabled();
        }

        public override string Author => "blackruby";
    }
}