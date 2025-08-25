using UnityEngine;

namespace ShutUpMonkeys.Behaviours
{
    internal class CreateMuteButton : MonoBehaviour
    {
        private void Start() => GorillaTagger.OnPlayerSpawned(() =>
        {
            Main.MuteButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Main.MuteButton.name = "MuteButton";
            Main.MuteButton.layer = 18;

            Main.MuteButton.transform.localScale = new Vector3(0.115f, 0.115f, 0.07f);
            Main.MuteButton.transform.position = new Vector3(-60.3478f, 4.7193f, -61.0687f);
            Main.MuteButton.transform.rotation = Quaternion.Euler(8.4964f, 31.8166f, 0f);

            Main.MuteButton.GetComponent<BoxCollider>().isTrigger = true;
            Main.MuteButton.AddComponent<MuteTrigger>();
            Main.MuteButton.AddComponent<PlayerStuff>();
        });

        internal class MuteTrigger : GorillaPressableButton
        {
            public override void ButtonActivationWithHand(bool isLeftHand)
            {
                Main.IsLobbyMuted = !Main.IsLobbyMuted;
                Main.MuteEveryone();
            }
        }
    }
}