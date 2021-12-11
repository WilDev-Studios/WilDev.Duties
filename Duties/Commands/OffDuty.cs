using System;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;
using OpenMod.Unturned.Commands;
using OpenMod.API.Permissions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using SDG.Unturned;

namespace Duties.Commands
{
    [Command("offduty")]
    [CommandAlias("offd")]
    [CommandDescription("Go off duty")]
    [CommandActor(typeof(UnturnedUser))]
    public class OffDutyCommand : UnturnedCommand
    {
        private readonly IPermissionChecker m_PermissionChecker;
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;

        public OffDutyCommand(
            IPermissionChecker permissionChecker,
            IConfiguration configuration,
            IStringLocalizer stringLocalizer,
            IServiceProvider serviceProvider
            ) : base(serviceProvider)
        {
            m_PermissionChecker = permissionChecker;
            m_Configuration = configuration;
            m_StringLocalizer = stringLocalizer;
        }

        protected override async UniTask OnExecuteAsync()
        {
            UnturnedUser ExecutedPlayer = (UnturnedUser)Context.Actor;

            // Make sure the Player has the permissions to execute the command
            if (await m_PermissionChecker.CheckPermissionAsync(ExecutedPlayer, "OnDuty") != PermissionGrantResult.Grant)
            {
                throw new NotEnoughPermissionException((OpenMod.API.Localization.IOpenModStringLocalizer)this, "OnDuty");
            }

            // Make sure the Player is actually in that job, to prevent spam
            if (!OnDutyCommand.CurrentPlayerJobs.Contains(ExecutedPlayer))
            {
                await ExecutedPlayer.PrintMessageAsync(m_StringLocalizer["No-Current-Job"]);
                return;
            }

            // Clears Player from the Dictionary
            OnDutyCommand.CurrentPlayerJobs.Remove(ExecutedPlayer);

            ExecutedPlayer.Player.SteamPlayer.playerID.characterName = OnDutyCommand.PlayerDisplayNames[ExecutedPlayer];

            // Sends a message to all players saying that the Player is now off duty from that job
            ChatManager.serverSendMessage(m_StringLocalizer["Off-Duty", new { Player = ExecutedPlayer.DisplayName }], UnityEngine.Color.green, null, null, EChatMode.GLOBAL, m_Configuration.GetSection("Messages-Image-URL").Get<string>(), true);
        }
    }
}
