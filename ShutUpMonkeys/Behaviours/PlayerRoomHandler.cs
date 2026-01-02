using Photon.Pun;
using Photon.Realtime;

namespace ShutUpMonkeys.Behaviours
{
    public class PlayerRoomHandler : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom() => Plugin.MaybeMuteEveryone();

        public override void OnPlayerEnteredRoom(Player p)
        {
            var lines = GorillaScoreboardTotalUpdater.allScoreboardLines;

            for (int i = 0; i < lines.Count; i++)
            {
                var line = lines[i];
                var isMuted = Plugin.IsLobbyMuted;

                if (line.linePlayer.UserId == p.UserId)
                {
                    line.muteButton.isOn = isMuted;
                    line.PressButton(isMuted, GorillaPlayerLineButton.ButtonType.Mute);
                    return;
                }
            }
        }
    }
}