using HarmonyLib;

namespace DoomScroll_V2.Patches
{
    [HarmonyPatch(typeof(HudManager))]
    public static class HudManagerPatch
    {
        [HarmonyPostfix]
        [HarmonyPatch("Start")]
        public static void PostfixStart()
        {
            ScreenshotManager.Instance.ReSet();
            FolderManager.Instance.Reset();
        }

        [HarmonyPostfix]
        [HarmonyPatch("Update")]
        public static void PostfixUpdate()
        {
            ScreenshotManager.Instance.CheckButtonClicks();
        }

        [HarmonyPostfix]
        [HarmonyPatch("SetHudActive")]
        public static void PostfixSetHudActive(bool isActive)
        {
            if (!ScreenshotManager.Instance.IsCameraOpen)
            {
                ScreenshotManager.Instance.ActivateCameraButton(isActive);
                DoomScroll._log.LogInfo("HudManager.SetHudActive ---- CAMERA ACTIVE: " + isActive);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("OpenMeetingRoom")]
        public static void PrefixOpenMeetingRoom()
        {
            DoomScroll._log.LogInfo("MEETING OPENED");
            if (ScreenshotManager.Instance.IsCameraOpen)
            {
                ScreenshotManager.Instance.ToggleCamera();
                DoomScroll._log.LogInfo("HudManager.OpenMeetingRoom ---- CAMERA CLOSED");
            }
        }
    }
}
