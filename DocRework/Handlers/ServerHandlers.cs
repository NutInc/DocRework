namespace DocRework.Handlers
{
    using AbilityControllers;
    using MEC;
    using static DocRework;

    public class ServerHandlers
    {
        public void OnRoundStart()
        {
            // Kill the EngageBuff coroutine once the round starts
            try
            {
                Timing.KillCoroutines("SCP049_Passive");
                Timing.KillCoroutines("SCP049_Active_Cooldown");
            }
            catch
            {
                //suppress
            }

            // Reset values to their default
            Scp049AbilityController.CureCounter = 0;
            Scp049AbilityController.AbilityCooldown = Instance.Config.DoctorConfigs.Cooldown;
        }
    }
}