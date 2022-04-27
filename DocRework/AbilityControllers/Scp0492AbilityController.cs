using Exiled.API.Enums;

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
            if (attacker.Role != RoleType.Scp0492 || target.Role.Team == Team.SCP)
                return;

            foreach (var ply in Player.List)
            {
                if (ply.Role.Team == Team.SCP && ply == attacker)
                    return;
                
                if (Vector3.Distance(attacker.Position, ply.Position) > 1.65f)
                    return;

                ply.Hurt(attacker, aoeDamage, DamageType.Scp049);
            }
        }
    }
}