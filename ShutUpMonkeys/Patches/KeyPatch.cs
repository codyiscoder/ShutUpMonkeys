using GorillaNetworking;
using HarmonyLib;

namespace ShutUpMonkeys
{
    [HarmonyPatch(typeof(GorillaKeyboardButton), "OnButtonPressedEvent")]
    internal class KeyPatch
    {
        static void Postfix(GorillaKeyboardButton __instance)
        {
            if (!Plugin.isInit || !Plugin.isOK || __instance.Binding != GorillaKeyboardBindings.option3) return;

            Plugin.IsLobbyMuted = !Plugin.IsLobbyMuted;
            Plugin.MaybeMuteEveryone();
        }
    }
}