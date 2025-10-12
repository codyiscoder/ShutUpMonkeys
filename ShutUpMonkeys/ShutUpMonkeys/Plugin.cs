using BepInEx;
using GorillaNetworking;
using HarmonyLib;
using TMPro;
using UnityEngine;

namespace ShutUpMonkeys
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        public static bool IsLobbyMuted = false;
        public static GameObject MuteButton;
        public static TextMeshPro buttonText;

        public static bool isOK => 
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Color &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Mic &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Turn &&
            GorillaComputer.instance.currentState != GorillaComputer.ComputerState.Queue;

        private void Awake()
        {
            var harmony = new Harmony(Constants.GUID);
            harmony.PatchAll();

            GorillaTagger.OnPlayerSpawned(() =>
            {
                MuteButton = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/TreeRoomInteractables/GorillaComputerObject/ComputerUI/keyboard (1)/Buttons/Keys/option 3/");
                buttonText = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom/option3/").GetComponent<TextMeshPro>();
            });
        }

        private void Update()
        {
            buttonText.text = isOK ? IsLobbyMuted ? "MUTED" : "UNMUTED" : "OPTION 3";
        }

        public static void DoTheThing()
        {
            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                l.muteButton.isOn = IsLobbyMuted;
                l.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
            }
        }
    }
}