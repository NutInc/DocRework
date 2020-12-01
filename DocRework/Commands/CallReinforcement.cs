namespace DocRework.Commands
{
    using AbilityControllers;
    using CommandSystem;
    using Exiled.API.Features;
    using System;

    [CommandHandler(typeof(ClientCommandHandler))]
    public class CallReinforcement : ICommand
    {
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var ply = Player.Get((sender as CommandSender)?.SenderId);
            response = Scp049AbilityController.CallZombieReinforcement(ply);
            return true;
        }

        public string Command => "cr";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "";
    }
}