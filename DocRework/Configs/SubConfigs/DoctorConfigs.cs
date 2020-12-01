namespace DocRework.Configs.SubConfigs
{
    using System.ComponentModel;

    public class DoctorConfigs
    {
        [Description("Allow SCP-049 to be healed for a percentage of it's missing health every player revival")]
        public bool AllowDocSelfHeal { get; set; } = true;

        [Description("Set the minimum cure amount for the buff area to kick in")]
        public int MinCures { get; set; } = 3;

        [Description("Change between 049's arua's heal type: 0 is for flat HP, 1 is for missing % HP")]
        public byte HealType { get; set; } = 0;

        [Description("Size of 049's healing radius")]
        public float HealRadius { get; set; } = 2.6f;

        [Description("The amount of HP the Doc heals their Zombies")]
        public float HealAmountFlat { get; set; } = 15.0f;

        [Description("The base amount of missing % HP the Doc heals their Zombies at the start of their buff")]
        public float ZomHealAmountPercentage { get; set; } = 10.0f;

        [Description("Multiplier for the ZomHealAmountPercentage value every time a Doctor revives someone")]
        public float HealPercentageMultiplier { get; set; } = 1.3f;

        [Description("Percentage of SCP-049's missing health to be healed")]
        public float DocMissingHealthPercentage { get; set; } = 15.0f;

        [Description("Cooldown for SCP049 active ability")]
        public ushort Cooldown { get; set; } = 180;
    }
}