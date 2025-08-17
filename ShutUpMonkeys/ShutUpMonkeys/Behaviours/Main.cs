using GorillaExtensions;
using Photon.Pun;
using Photon.Realtime;
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

            if (MuteButton != null)
                MuteButton.SetActive(NetworkSystem.Instance.InRoom);
        }

        public static void UpdateMuteState(Player p = null, bool muteEveryone = false)
        {
            if (GorillaScoreboardTotalUpdater.allScoreboardLines == null) return;

            if (muteEveryone)
            {
                foreach (var line in GorillaScoreboardTotalUpdater.allScoreboardLines)
                {
                    line.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                    if (line.muteButton != null)
                        line.muteButton.isOn = IsLobbyMuted;
                }
                return;
            }

            foreach (var line in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (line.linePlayer.UserId == p.UserId)
                {
                    line.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                    if (line.muteButton != null)
                        line.muteButton.isOn = IsLobbyMuted;
                }
            }
        }
    }

    public class PlayerEnter : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerEnteredRoom(Player newPlayer) => Main.UpdateMuteState(newPlayer);
    }
}