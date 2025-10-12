using GorillaNetworking;
using HarmonyLib;
using ShutUpMonkeys;

[HarmonyPatch(typeof(GorillaKeyboardButton), "OnButtonPressedEvent")]
internal class KeyPatch
{
    static void Postfix(GorillaKeyboardButton __instance)
    {
        if (__instance.Binding == GorillaKeyboardBindings.option3 && Plugin.isOK)
        {
            Plugin.IsLobbyMuted = !Plugin.IsLobbyMuted;
            Plugin.DoTheThing();
        }
    }
}