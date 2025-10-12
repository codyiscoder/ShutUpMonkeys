using Photon.Pun;
using Photon.Realtime;

namespace ShutUpMonkeys.Behaviours
{
    public class PlayerRoomHandler : MonoBehaviourPunCallbacks
    {
        public override void OnJoinedRoom() => Plugin.DoTheThing();

        public override void OnPlayerEnteredRoom(Player p)
        {
            foreach (var l in GorillaScoreboardTotalUpdater.allScoreboardLines)
            {
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