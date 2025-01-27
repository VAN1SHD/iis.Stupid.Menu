﻿using iiMenu.Notifications;
using HarmonyLib;
using Photon.Pun;
using UnityEngine;
using static iiMenu.Menu.Main;
using PlayFab.Internal;
using PlayFab;
using GorillaTag;

namespace iiMenu.Patches
{
    [HarmonyPatch(typeof(GorillaNot), "SendReport")]
    internal class AntiCheat : MonoBehaviour
    {
        private static bool Prefix(string susReason, string susId, string susNick)
        {
            if (AntiCheatSelf || AntiCheatAll)
            {
                if (susId == PhotonNetwork.LocalPlayer.UserId)
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=green>ANTICHEAT</color><color=grey>] </color><color=white>You have been reported for " + susReason + ".</color>");
                    susNick.Remove(PhotonNetwork.LocalPlayer.NickName.Length);
                    susId.Remove(PhotonNetwork.LocalPlayer.UserId.Length);
                    RPCProtection();
                }
                else
                {
                    if (AntiCheatAll)
                    {
                        NotifiLib.SendNotification("<color=grey>[</color><color=green>ANTICHEAT</color><color=grey>] </color><color=white>" + susNick + " was reported for " + susReason + ".</color>");
                    }
                }
            }
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "LogErrorCount")]
    public class NoLogErrorCount : MonoBehaviour
    {
        private static bool Prefix(string logString, string stackTrace, LogType type)
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "CloseInvalidRoom")]
    public class NoCloseInvalidRoom : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "CheckReports", MethodType.Enumerator)]
    public class NoCheckReports : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "QuitDelay", MethodType.Enumerator)]
    public class NoQuitDelay : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "IncrementRPCTracker")]
    public class NoIncrementRPCTracker : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "IncrementRPCCallLocal")]
    public class NoIncrementRPCCallLocal : MonoBehaviour
    {
        private static bool Prefix(PhotonMessageInfo info, string rpcFunction)
        {
            // Debug.Log(info.Sender.NickName + " sent rpc: " + rpcFunction);
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "GetRPCCallTracker")]
    internal class NoGetRPCCallTracker : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "IncrementRPCCall")]
    public class NoIncrementRPCCall : MonoBehaviour
    {
        private static bool Prefix(PhotonMessageInfo info, string callingMethod = "")
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(VRRig), "IncrementRPC")]
    public class NoIncrementRPC : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayFabDeviceUtil), "SendDeviceInfoToPlayFab")]
    internal class PlayfabUtil01 : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayFabHttp), "InitializeScreenTimeTracker")]
    internal class PlayfabUtil02 : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(PlayFabClientInstanceAPI), "ReportDeviceInfo")]
    internal class PlayfabUtil03 : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "DispatchReport")]
    internal class NoDispatchReport : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(GorillaNot), "ShouldDisconnectFromRoom")]
    internal class NoShouldDisconnectFromRoom : MonoBehaviour
    {
        private static bool Prefix()
        {
            return false;
        }
    }

    [HarmonyPatch(typeof(ScienceExperimentPlatformGenerator), "SpawnSodaBubbleRPC")]
    public class AntiCrashPatch
    {
        public static bool Prefix(Vector2 surfacePosLocal, float spawnSize, float lifetime, double spawnTime)
        {
            if (AntiCrashToggle)
            {
                return false;
            }
            return true;
        }
    }
    /*
    [HarmonyPatch(typeof(GorillaGameManager), "LaunchSlingshotProjectile")]
    public class AntiCrash
    {
        private static bool Prefix(Vector3 slingshotLaunchLocation, Vector3 slingshotLaunchVelocity, int projHash, int trailHash, bool forLeftHand, int projectileCount, bool shouldOverrideColor, float colorR, float colorG, float colorB, float colorA, PhotonMessageInfo info)
        {
            if (AntiCrashToggle)
            {
                if (info.Sender != PhotonNetwork.LocalPlayer)
                {
                    if (Vector3.Distance(GorillaGameManager.instance.FindPlayerVRRig(info.Sender).transform.position, slingshotLaunchLocation) > 1.5f)
                    {
                        return false;
                    }
                    if (Vector3.Distance(slingshotLaunchLocation, GorillaLocomotion.Player.Instance.transform.position) > 10)
                    {
                        return false;
                    }
                    if (ObjectPools.instance.GetPoolByHash(projHash).objectToPool.GetComponent<SlingshotProjectileTrail>() != null)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }*/
}
