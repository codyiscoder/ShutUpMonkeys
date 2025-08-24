using BepInEx;
using ExitGames.Client.Photon;
using Photon.Pun;
using ShutUpMonkeys.Behaviours;

namespace ShutUpMonkeys
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake() => gameObject.AddComponent<Main>();

        private void Start()
        {
            Hashtable prop = new Hashtable();
            prop.Add("cody likes burritos", $"{Constants.NAME}v{Constants.VERS}");
            PhotonNetwork.LocalPlayer.SetCustomProperties(prop);
        }
    }

    internal static class Constants
    {
        public const string GUID = "net.cody.shutupmonkeys";
        public const string NAME = "ShutUpMonkeys";
        public const string VERS = "1.1.0";
    }
}