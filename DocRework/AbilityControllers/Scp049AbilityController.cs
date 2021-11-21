namespace DocRework.AbilityControllers
{
    using Configs;
    using Exiled.API.Features;
    using Hints;
    using MEC;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using static DocRework;

    public class Scp049AbilityController
    {
        private static Config Config => Instance.Config;

        public static uint CureCounter = 0;
        public static ushort AbilityCooldown;
        private static readonly float Radius = Config.DoctorConfigs.HealRadius;
        private static readonly float HealAmountFlat = Config.DoctorConfigs.HealAmountFlat;
        public static float HealAmountPercentage = Config.DoctorConfigs.ZomHealAmountPercentage;
        private static readonly byte HealType = Config.DoctorConfigs.HealType;

        public static IEnumerator<float> EngageBuff()
        {
            while (true)
            {
                // Check EVERY zombies' position for EVERY Doctor.
                foreach (Player scp049 in Player.Get(RoleType.Scp049))
                foreach (Player scp0492 in Player.Get(RoleType.Scp0492))

                    // Check if a Zombie (Z) is inside of a aura drawn around the Doctor (D)
                    if (Vector3.Distance(scp049.Position, scp0492.Position) <= Radius)
                        ApplyHeal(HealType, scp0492, HealAmountFlat, HealAmountPercentage);


                yield return Timing.WaitForSeconds(5f);
            }
        }

        private static void ApplyHeal(byte type, Player p, float flat, float multiplier)
        {
            float hpGiven;
            bool canDisplay = true;
            float missingHp = p.MaxHealth - p.Health;
            float adjMultiplier = multiplier / 100;

            if (p.Health == p.MaxHealth)
                canDisplay = false;

            // Flat hp
            if (type == 0)
            {
                if (p.Health + flat > p.MaxHealth)
                {
                    hpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    hpGiven = flat;
                    p.Health += flat;
                }
            }

            // Percentage HP
            else
            {
                if (p.Health + missingHp * adjMultiplier > p.MaxHealth)
                {
                    hpGiven = p.MaxHealth - p.Health;
                    p.Health = p.MaxHealth;
                }
                else
                {
                    hpGiven = missingHp * adjMultiplier;
                    p.Health += missingHp * adjMultiplier;
                }
            }

            // Sent Zombies notification that they got healed.
            if (canDisplay)
                p.HintDisplay.Show(new TextHint($"<color=red>+{hpGiven} HP</color>",
                    new HintParameter[] {new StringHintParameter("")}, null, 2f));
        }

        public static void ApplySelfHeal(Player p, float missing)
        {
            float missingHp = p.MaxHealth - p.Health;
            if (p.Health + missingHp * missing > p.MaxHealth) p.Health = p.MaxHealth;
            else p.Health += missingHp * missing;
        }

        public static string CallZombieReinforcement(Player ply)
        {
            // List of spectators to randomly choose from later
            List<Player> list = Player.Get(RoleType.Spectator).ToList();

            // Only 049 is allowed to use this command
            if (ply.Role != RoleType.Scp049)
                return Config.Translations.ActivePermissionDenied;

            if (CureCounter < Config.DoctorConfigs.MinCures)
                return Config.Translations.ActiveNotEnoughRevives;

            // Pretty self-explanatory i think
            if (AbilityCooldown > 0)
                return Config.Translations.ActiveOnCooldown + AbilityCooldown;

            // If the list is empty it means no spectators can be chosen.
            if (list.IsEmpty())
                return Config.Translations.ActiveNoSpectators;

            // Get a random player from the spectator list and spawn it as 049-2 then tp it to doc.
            var index = 0;
            index += new System.Random().Next(list.Count);
            var selected = list[index];

            selected.SetRole(RoleType.Scp0492);
            selected.Health = selected.MaxHealth;

            Timing.CallDelayed(0.5f,
                () => { selected.Position = new Vector3(ply.Position.x, ply.Position.y, ply.Position.z); });

            AbilityCooldown = Config.DoctorConfigs.Cooldown;
            Timing.RunCoroutine(StartCooldownTimer(), "SCP049_Active_Cooldown");
            return Config.Translations.SuccessfulRevive.ReplaceAfterToken('$',
                new[] {new Tuple<string, object>("RevivedPlayer", selected.Nickname)});
        }

        public static IEnumerator<float> StartCooldownTimer()
        {
            while (AbilityCooldown != 0)
            {
                AbilityCooldown--;
                yield return Timing.WaitForSeconds(1f);
            }

            // Notify the Doc that the ability's cd has expired.
            foreach (Player scp049 in Player.Get(RoleType.Scp049))
                scp049.HintDisplay.Show(new TextHint(Config.Translations.ActiveReadyNotification,
                    new HintParameter[] {new StringHintParameter("")}, null, 5f));

            // Kill it just for sure.
            Timing.KillCoroutines("SCP049_Active_Cooldown");
        }
    }
}