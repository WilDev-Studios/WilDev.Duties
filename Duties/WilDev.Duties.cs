using System;
using Microsoft.Extensions.Logging;
using Cysharp.Threading.Tasks;
using OpenMod.Unturned.Plugins;
using OpenMod.API.Plugins;

[assembly: PluginMetadata("WilDev.Duties", DisplayName = "WilDev Studios' Duty Plugin", Author = "WilDev Studios", Website = "https://discord.gg/4Ggybyy87d")]
namespace Duties
{
    public class Duties : OpenModUnturnedPlugin
    {
        private readonly ILogger<Duties> m_Logger;

        public Duties(
            ILogger<Duties> logger,
            IServiceProvider serviceProvider) : base(serviceProvider)
        {
            m_Logger = logger;
        }

        protected override UniTask OnLoadAsync()
        {
            m_Logger.LogInformation("+==========================================================+");
            m_Logger.LogInformation("| WILDEV.DUTIES plugin has been loaded!                    |");
            m_Logger.LogInformation("| Made with <3 by WildKadeGaming @ WilDev Studios          |");
            m_Logger.LogInformation("| WilDev Discord: https://discord.com/invite/4Ggybyy87d    |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }

        protected override UniTask OnUnloadAsync()
        {
            m_Logger.LogInformation("+==========================================================+");
            m_Logger.LogInformation("| WILDEV.DUTIES plugin has been unloaded!                  |");
            m_Logger.LogInformation("| Made with <3 by WildKadeGaming @ WilDev Studios          |");
            m_Logger.LogInformation("| WilDev Discord: https://discord.com/invite/4Ggybyy87d    |");
            m_Logger.LogInformation("+==========================================================+");
            return UniTask.CompletedTask;
        }
    }
}
