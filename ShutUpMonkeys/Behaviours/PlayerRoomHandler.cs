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
                var l = lines[i];

                if (l.linePlayer.UserId == p.UserId)
                {
                    l.muteButton.isOn = Plugin.IsLobbyMuted;
                    l.PressButton(Plugin.IsLobbyMuted, GorillaPlayerLineButton.ButtonType.Mute);
                    return;
                }
            }
        }
    }
}