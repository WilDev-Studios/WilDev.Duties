using System;
using Cysharp.Threading.Tasks;
using OpenMod.Core.Commands;
using OpenMod.Unturned.Users;
using OpenMod.Unturned.Commands;
using OpenMod.API.Permissions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;
using SDG.Unturned;

namespace Duties.Commands
{
    [Command("onduty")]
    [CommandAlias("ond")]
    [CommandDescription("Go on duty")]
    [CommandActor(typeof(UnturnedUser))]
    [CommandSyntax("<job>")]
    public class OnDutyCommand : UnturnedCommand
    {
        private readonly IPermissionChecker m_PermissionChecker;
        private readonly IConfiguration m_Configuration;
        private readonly IStringLocalizer m_StringLocalizer;

        public static List<UnturnedUser> CurrentPlayerJobs = new List<UnturnedUser>();
        public static Dictionary<UnturnedUser, string> PlayerDisplayNames = new Dictionary<UnturnedUser, string>();

        public OnDutyCommand(
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
            var RequestedJob = await Context.Parameters.GetAsync<string>(0);
            var JobsList = m_Configuration.GetSection("Jobs-List").Get<List<string>>();

            // Make sure the Player has the permissions to execute the command
            if (await m_PermissionChecker.CheckPermissionAsync(ExecutedPlayer, "OnDuty") != PermissionGrantResult.Grant)
            {
                throw new NotEnoughPermissionException((OpenMod.API.Localization.IOpenModStringLocalizer)this, "OnDuty");
            }

            // Makes sure the Requested Job is not empty
            if (RequestedJob == null)
            {
                throw new CommandWrongUsageException("A job parameter must be included");
            }

            // Makes sure the Requested Job actually exists
            if (!JobsList.Contains(RequestedJob.ToLower()))
            {
                await ExecutedPlayer.PrintMessageAsync(m_StringLocalizer["No-Job-Found"]);
                return;
            }

            CurrentPlayerJobs.Add(ExecutedPlayer);
            
            PlayerDisplayNames.Add(ExecutedPlayer, ExecutedPlayer.DisplayName);
            ExecutedPlayer.Player.SteamPlayer.playerID.characterName = "[" + RequestedJob.ToUpper() + "] " + ExecutedPlayer.DisplayName;

            // Sends a message to all players saying that the Player is now on duty in that job
            ChatManager.serverSendMessage(m_StringLocalizer["On-Duty:" + RequestedJob.ToUpper(), new { Player = ExecutedPlayer.DisplayName }], UnityEngine.Color.green, null, null, EChatMode.GLOBAL, m_Configuration.GetSection("Messages-Image-URL").Get<string>(), true);
        }
    }
}
