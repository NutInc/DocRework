namespace DocRework.AbilityControllers
{
    using Exiled.API.Features;
    using System.Linq;
    using UnityEngine;

    public class Scp0492AbilityController
    {
        public static void DealAoeDamage(Player attacker, Player target, float aoeDamage)
        {
            // Check if the attacker is a zombie and if the target is not an scp
            if (attacker.Role != RoleType.Scp0492 || target.Team == Team.SCP)
                return;

            foreach (Player ply in Player.List.Where(player => player.Team != Team.SCP && player != attacker))
            {
                if (Vector3.Distance(attacker.Position, ply.Position) > 1.65f)
                    return;

                ply.Hurt(aoeDamage, DamageTypes.Scp0492, attacker.Nickname, attacker.Id);
            }
        }
    }
}