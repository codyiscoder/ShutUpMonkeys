using GorillaExtensions;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ShutUpMonkeys.Behaviours
{
    internal class Main : MonoBehaviour
    {
        public static bool IsLobbyMuted = false;
        public static GameObject MuteButton;

        private void Start() => gameObject.AddComponent<CreateMuteButton>();

        private void Update()
        {
            if (MuteButton == null) return;

            MuteButton.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
            MuteButton.GetOrAddComponent<MeshRenderer>().material.color = IsLobbyMuted ? Color.red : Color.white;
        }

        public static void MuteEveryone()
        {
            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                l.muteButton.isOn = IsLobbyMuted;
                l.PressButton(IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
            }
        }
    }

    public class PlayerStuff : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom() => Main.MuteEveryone();

        public override void OnPlayerEnteredRoom(Player p)
        {
            // terrible way to do this but 🤫
            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
                if (l.linePlayer.UserId == p.UserId)
                {
                    l.muteButton.isOn = Main.IsLobbyMuted;
                    l.PressButton(Main.IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                }
            }
        }
    }
}