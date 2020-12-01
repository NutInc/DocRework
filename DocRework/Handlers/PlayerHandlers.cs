namespace DocRework.Handlers
{
    using AbilityControllers;
    using Exiled.Events.EventArgs;
    using static DocRework;

    public class PlayerHandlers
    {
        public void OnPlayerHurt(HurtingEventArgs ev)
        {
            if (Instance.Config.ZombieConfigs.AllowZombieAoe &&
                Scp049AbilityController.CureCounter >= Instance.Config.DoctorConfigs.MinCures)
                Scp0492AbilityController.DealAoeDamage(ev.Attacker, ev.Target,
                    Instance.Config.ZombieConfigs.ZombieAoeDamage);
        }
    }
}