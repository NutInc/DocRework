namespace DocRework.Configs.SubConfigs
{
    using System.ComponentModel;

    public class Translations
    {
        [Description("Message sent to SCP-049 upon reaching the minimum cures amount required")]
        public string PassiveActivationMessage { get; set; } =
            "<color=red>Your passive ability is now activated.\nYou now heal zombies around you every 5 seconds.</color>";

        [Description("Message sent when you try to execute the .cr command when you're not a doctor")]
        public string ActivePermissionDenied { get; set; } = "You are not allowed to use this command!";

        [Description(
            "Message sent when you try to execute the .cr command but you don't yet have the min required revives")]
        public string ActiveNotEnoughRevives { get; set; } =
            "You don't have enough revives to use this ability!";

        [Description("Message sent when you try to execute the .cr command while it's on cooldown")]
        public string ActiveOnCooldown { get; set; } = "Can't use this yet! Cooldown remaining: ";

        [Description("Message send when there are no spectators to spawn")]
        public string ActiveNoSpectators { get; set; } =
            "Sorry, but we were unable to find any spectators for you. :(";

        [Description("Hint displayed when the .cr ability's cooldown has expired")]
        public string ActiveReadyNotification { get; set; } =
            "<color=green>You can now use your active ability.\nUse .cr in your console to spawn a zombie from the spectators.</color>";

        [Description("Message sent when the .cr command is executed successfully.")]
        public string SuccessfulRevive { get; set; } = "Revived $RevivedPlayer successfully.";
    }
}