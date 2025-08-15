using BepInEx;
using ShutUpMonkeys.Behaviours;

namespace ShutUpMonkeys
{
    [BepInPlugin(Constants.GUID, Constants.NAME, Constants.VERS)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake() => gameObject.AddComponent<Main>();
    }

    internal static class Constants
    {
        public const string GUID = "net.cody.shutupmonkeys";
        public const string NAME = "ShutUpMonkeys";
        public const string VERS = "1.0.0";
    }
}