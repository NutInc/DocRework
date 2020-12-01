namespace DocRework.Configs.SubConfigs
{
    using System.ComponentModel;

    public class ZombieConfigs
    {
        [Description("Allow SCP-049-2 to damage everyone around upon hitting an enemy target")]
        public bool AllowZombieAoe { get; set; } = true;

        [Description("Amount of health each person in 049-2's range loses by 049-2's AOE attack")]
        public float ZombieAoeDamage { get; set; } = 15.0f;
    }
}