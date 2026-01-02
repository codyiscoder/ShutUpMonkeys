using GorillaNetworking;
using HarmonyLib;

namespace ShutUpMonkeys
{
    [HarmonyPatch(typeof(GorillaKeyboardButton), "OnButtonPressedEvent")]
    internal class KeyPatch
    {
        static void Postfix(GorillaKeyboardButton __instance)
        {
            if (__instance.Binding != GorillaKeyboardBindings.option3 || !Plugin.isOK) return;

            Plugin.IsLobbyMuted = !Plugin.IsLobbyMuted;
            Plugin.MaybeMuteEveryone();
        }
    }
}