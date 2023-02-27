using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;

namespace DoomScroll_V2
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
   // [BepInProcess("Among Us.exe")]
    public class DoomScrollPlugin : BasePlugin
    {
        public override void Load()
        {
            // Plugin startup logic
            Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} is loaded!");

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();
        }
    }
}