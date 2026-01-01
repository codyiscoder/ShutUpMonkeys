using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ShutUpMonkeys
{
    [BepInPlugin("com.cody.shutupmonkeys", "ShutUpMonkeys", "2.1.0")]
    public class Plugin : BaseUnityPlugin
    {
        public static bool IsLobbyMuted, isInit;
        public static GameObject MuteButton;
        public static TextMeshPro buttonText;

        public static bool isOK =>
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Color &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Mic &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Turn &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Queue;

        private void Start()
        {
            var harmony = new Harmony("com.cody.shutupmonkeys");
            harmony.PatchAll();

            GorillaTagger.OnPlayerSpawned(OnGameInit);
        }

        private void OnGameInit()
        {
            string TreeRoom = "Environment Objects/LocalObjects_Prefab/TreeRoom";
            MuteButton = GameObject.Find($"{TreeRoom}/TreeRoomInteractables/GorillaComputerObject/ComputerUI/keyboard (1)/Buttons/Keys/option 3/");
            buttonText = GameObject.Find($"{TreeRoom}/TreeRoom/option3/").GetComponent<TextMeshPro>();
            isInit = true;
        }

        public static void MaybeMuteEveryone()
        {
            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                l.muteButton.isOn = IsLobbyMuted;
                l.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                ChangeText(isOK ? IsLobbyMuted ? "MUTED" : "UNMUTED" : "OPTION 3");
            }
        }

        public static void ChangeText(string txt) => buttonText.text = txt;
    }
}