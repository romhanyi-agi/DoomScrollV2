﻿using HarmonyLib;
using Hazel;
using System;
using Il2CppSystem.Collections.Generic;
using System.Linq;
using static GameData;

namespace DoomScroll_V2.Patches
{
    public enum CustomRPC : byte
    {
        DEATHNOTE = 252,
        SENDASSIGNEDTASK = 253,
        SENDSWC = 254,
        SENDIMAGE = 255
    }

    [HarmonyPatch(typeof(PlayerControl))]
    public static class PlayerControlPatch
    {
        static int i = 0;

        [HarmonyPostfix]
        [HarmonyPatch(typeof(PlayerControl._CoSetTasks_d__113), nameof(PlayerControl._CoSetTasks_d__113.MoveNext))]
        public static void PostfixCoSetTasks(PlayerControl._CoSetTasks_d__113 __instance)
        {
            List<PlayerTask> tasks = __instance.__4__this.myTasks;
            // check for impostor
            if (tasks != null && tasks.Count > 0)
            {
                if (__instance.__4__this.AmOwner && PlayerControl.LocalPlayer.Data.Role.Role != AmongUs.GameOptions.RoleTypes.Impostor)
                {
                    TaskAssigner.Instance.SelectRandomTasks(tasks);
                    DoomScroll._log.LogInfo("SelectRandomTasks Function called " + i++ + " times");
                }
            }
        }

        [HarmonyPostfix]
        [HarmonyPatch("CompleteTask")]
        public static void PostfixCompleteTasks(PlayerControl __instance, uint idx)
        {
            TaskInfo taskInfo = __instance.Data.FindTaskById(idx);
            if (taskInfo == null)
            {
                DoomScroll._log.LogInfo("Task not found: " + idx.ToString());
                return;
            }
            if (taskInfo.Complete && TaskAssigner.Instance.AssignableTasksIDs.Contains(idx))
            {
                TaskAssigner.Instance.AssignPlayerToTask(idx);
            }
        }

       /* [HarmonyPostfix]
        [HarmonyPatch("Die")]
        public static void PostfixDie(PlayerControl __instance, DeathReason reason)
        {
            // local player dead, has to update swc list
            SecondaryWinConditionManager.UpdateSWCList(__instance.PlayerId, reason);
        }*/

        [HarmonyPrefix]
        [HarmonyPatch("HandleRpc")]
        public static void PrefixHandleRpc([HarmonyArgument(0)] byte callId, [HarmonyArgument(1)] MessageReader reader, PlayerControl __instance)
        {
            switch (callId)
            {
                /*case (byte)CustomRPC.DEATHNOTE:
                    {
                        // other player dead, has to update swc list
                        SecondaryWinConditionManager.UpdateSWCList(reader.ReadByte(), (DeathReason)reader.ReadByte());
                        return;
                    }*/
                case (byte)CustomRPC.SENDASSIGNEDTASK:
                    {
                        TaskAssigner.Instance.AddToAssignedTasks(__instance, reader.ReadByte(), reader.ReadUInt32());
                        return;
                    }
                /*case (byte)CustomRPC.SENDSWC:
                    {
                        SecondaryWinConditionManager.addToPlayerSWCList(new SecondaryWinCondition(reader.ReadByte(), (Goal)reader.ReadByte(), reader.ReadByte()));
                        return;
                    }*/
                case (byte)CustomRPC.SENDIMAGE:
                    {
                        byte[] imageBytes = reader.ReadBytesAndSize();
                        // DoomScroll._log.LogInfo("Image received! Size:" + imageBytes.Length);
                        if (DestroyableSingleton<HudManager>.Instance)
                        {
                            ImageSender.AddChat(__instance, imageBytes);
                            return;
                        }
                        break;
                    }
            }
        }
    }
}
