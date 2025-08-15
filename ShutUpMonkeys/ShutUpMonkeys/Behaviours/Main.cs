using GorillaExtensions;
using UnityEngine;

namespace ShutUpMonkeys.Behaviours
{
    internal class Main : MonoBehaviour
    {
        public static bool IsLobbyMuted;
        public static GameObject MuteButton;

        private void Start() => gameObject.AddComponent<CreateMuteButton>();

        private void Update()
        {
            if (MuteButton != null)
                MuteButton.GetOrAddComponent<MeshRenderer>().material.color = IsLobbyMuted ? Color.red : Color.white;

            if (NetworkSystem.Instance != null)
            {
                IsLobbyMuted = NetworkSystem.Instance.InRoom ? IsLobbyMuted : false;

                if (MuteButton != null)
                    MuteButton.SetActive(NetworkSystem.Instance.InRoom);
            }

            if (GorillaScoreboardTotalUpdater.allScoreboardLines != null)
            {
                foreach (var line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    line.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                    if (line.muteButton != null)
                        line.muteButton.isOn = IsLobbyMuted;
                }
            }
        }
    }
}