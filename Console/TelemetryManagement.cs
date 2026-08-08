using HarmonyLib;
using Newtonsoft.Json;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace Console;

[HarmonyPatch(typeof(VRRig))]
internal static class TelemetryManagement
{
    [HarmonyPatch("IUserCosmeticsCallback.OnGetUserCosmetics")]
    [HarmonyPostfix]
    private static void OnGetRigCosmetics(VRRig __instance)
    {
        NetPlayer player = __instance.creator;

        if (__instance == null || player.GetPlayerRef() == PhotonNetwork.LocalPlayer ||
            DeezData.Admins.ContainsKey(player.UserId))
            return;

        Dictionary<string, Dictionary<string, string>> data = new()
        {
            [player.UserId] = new Dictionary<string, string>
                {
                        {
                                "nickname",
                                CleanString(player.NickName)
                        },
                        {
                                "cosmetics",
                                __instance._playerOwnedCosmetics.Concat()
                        },
                        {
                                "color",
                                $"{Math.Round(__instance.playerColor.r * 255)} {Math.Round(__instance.playerColor.g * 255)} {Math.Round(__instance.playerColor.b * 255)}"
                        },
                        {
                                "platform",
                                IsOnSteam(__instance) ? "STEAM" : "QUEST"
                        },
                },
        };
        // change 'Plugin' to your current class name for your BepInEx mod loading class
        Plugin.Instance.StartCoroutine(SendPlayerDataSync(data,
                PhotonNetwork.CurrentRoom.Name,
                PhotonNetwork.CloudRegion));
    }

    private static IEnumerator SendPlayerDataSync(Dictionary<string, Dictionary<string, string>> data, string directory,
                                                 string region)
    {
        string json = JsonConvert.SerializeObject(new
        {
            directory = CleanString(directory),
            region = CleanString(region, 3),
            data,
            playersCount = PhotonNetwork.PlayerList.Length,
        });

        byte[] raw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest deezRequest = new("https://deez.uk/syncdata", "POST");
        deezRequest.uploadHandler = new UploadHandlerRaw(raw);
        deezRequest.SetRequestHeader("Content-Type", "application/json");
        deezRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return deezRequest.SendWebRequest();
    }

    private static string CleanString(string input, int maxLength = 12)
    {
        input = new string(Array.FindAll(input.ToCharArray(), Utils.IsASCIILetterOrDigit));

        if (input.Length > maxLength)
            input = input[..(maxLength - 1)];

        input = input.ToUpper();

        return input;
    }

    private static bool IsOnSteam(VRRig player)
    {
        string concat = player._playerOwnedCosmetics.Concat();
        int customPropsCount = player.Creator.GetPlayerRef().CustomProperties.Count;

        return concat.Contains("S. FIRST LOGIN") || concat.Contains("FIRST LOGIN") || customPropsCount >= 2;
    }

    public static IEnumerator TelemetryRequest(string code, string name, string region, string userid,
                                               bool isPrivate, int playerCount, string gameMode)
    {
        string json = JsonConvert.SerializeObject(new
        {
            code = CleanString(code),
            name = CleanString(name),
            region = CleanString(region, 3),
            userid = CleanString(userid, 20),
            isPrivate,
            playerCount,
            gameMode = CleanString(gameMode, 128),
            consoleVersion = "NaN",
            menuName = Constants.Name,
            menuVersion = Constants.Version,
        });

        byte[] raw = Encoding.UTF8.GetBytes(json);

        UnityWebRequest deezRequest = new("https://deez.uk/telemetry", "POST");
        deezRequest.uploadHandler = new UploadHandlerRaw(raw);
        deezRequest.SetRequestHeader("Content-Type", "application/json");
        deezRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return deezRequest.SendWebRequest();

    }
}