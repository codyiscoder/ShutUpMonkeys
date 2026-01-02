using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using PlayFab;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ShutUpMonkeys
{
    [BepInPlugin("com.cody.shutupmonkeys", "ShutUpMonkeys", "2.2.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static bool IsLobbyMuted;
        public static TextMeshPro buttonText;

        public static bool isOK =>
            NetworkSystem.Instance.InRoom &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Color &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Mic &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Turn &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Queue;

        private void Start()
        {
            var harmony = new Harmony("com.cody.shutupmonkeys");
            harmony.PatchAll();

            NetworkSystem.Instance.OnJoinedRoomEvent += RefreshText;
            NetworkSystem.Instance.OnReturnedToSinglePlayer += RefreshText;

            StartCoroutine(WaitForPlayFab());
        }

        // BEFORE YOU FLAME ME FOR THIS..... GorillaTagger.OnPlayerSpawned didnt work for this :/
        // so im using this 
        private IEnumerator WaitForPlayFab()
        {
            float time = 5f;
            float current = 0f;

            while (!PlayFabClientAPI.IsClientLoggedIn())
            {
                if (current >= time)
                    yield break;

                current += Time.deltaTime;
                yield return null;
            }

            OnGameInit();
        }

        public static void OnGameInit() => buttonText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/option3").GetComponent<TextMeshPro>();

        public static void RefreshText() => buttonText.text = isOK ? IsLobbyMuted ? "MUTED" : "UNMUTED" : "OPTION 3";

        public static void MaybeMuteEveryone()
        {
            if (!NetworkSystem.Instance.InRoom) return;

            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                l.muteButton.isOn = IsLobbyMuted;
                l.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                RefreshText();
            }
        }
    }
}