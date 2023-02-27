using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using System.Reflection;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DoomScroll_V2
{

    [BepInPlugin(MyPluginInfo.PLUGIN_GUID, MyPluginInfo.PLUGIN_NAME, MyPluginInfo.PLUGIN_VERSION)]
   // [BepInProcess("Among Us.exe")]
    public class DoomScroll : BasePlugin
    {
        public static ManualLogSource _log;
        public override void Load()
        {
            // Plugin startup logic
            _log = Log;
            _log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_NAME} is loaded!");

            var harmony = new Harmony(MyPluginInfo.PLUGIN_GUID);
            harmony.PatchAll();

            // Add mod stamp
            SceneManager.add_sceneLoaded((UnityAction<Scene, LoadSceneMode>)((scene, _) =>
            {
                if (scene.name == "MainMenu")
                {
                    ModManager.Instance.ShowModStamp();
                }
            }));
        }
    }
}