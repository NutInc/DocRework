namespace DocRework.Handlers
{
    using AbilityControllers;
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;
    using Hints;
    using MEC;
    using static DocRework;

    public class Scp049Handlers
    {
        public void OnFinishingRecall(FinishingRecallEventArgs ev)
        {
            // Check if round is still in progress
            if (!RoundSummary.RoundInProgress())
                return;

            // Counter for every player the Doctor has cured.
            Scp049AbilityController.CureCounter++;

            if (Scp049AbilityController.CureCounter == Instance.Config.DoctorConfigs.MinCures)
            {
                // Notify the Doctor that the buff is now active.
                foreach (Player scp049 in Player.Get(RoleType.Scp049))
                    scp049.HintDisplay.Show(new TextHint(Instance.Config.Translations.PassiveActivationMessage,
                        new HintParameter[] {new StringHintParameter("")}, null, 5f));

                // Run the actual EngageBuff corouting every 5 seconds.
                Timing.RunCoroutine(Scp049AbilityController.EngageBuff(), "SCP049_Passive");
                Timing.RunCoroutine(Scp049AbilityController.StartCooldownTimer(), "SCP049_Active_Cooldown");
            }

            // Increase the percentage zombies get healed for by the HealthPercentageMultiplier if the config option is set to 1 (percentage of missing hp mode)
            if (Instance.Config.DoctorConfigs.HealType == 1 &&
                Scp049AbilityController.CureCounter > Instance.Config.DoctorConfigs.MinCures)
                Scp049AbilityController.HealAmountPercentage *= Instance.Config.DoctorConfigs.HealPercentageMultiplier;

            // Heal the doctor for the configured percentage if it's missing health if the config option for it is set to true
            if (Instance.Config.DoctorConfigs.AllowDocSelfHeal)
                Scp049AbilityController.ApplySelfHeal(ev.Scp049,
                    Instance.Config.DoctorConfigs.DocMissingHealthPercentage);
        }
    }
}