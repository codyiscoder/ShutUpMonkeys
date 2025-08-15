using GorillaExtensions;
using UnityEngine;

namespace ShutUpMonkeys.Behaviours
{
    internal class CreateMuteButton : MonoBehaviour
    {
        private void Awake() => GorillaTagger.OnPlayerSpawned(() =>
        {
            if (Main.MuteButton != null) return;

            Main.MuteButton = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Main.MuteButton.name = "MuteButton";
            Main.MuteButton.layer = 18;
            Main.MuteButton.transform.localScale = new Vector3(0.115f, 0.115f, 0.07f);
            Main.MuteButton.transform.position = new Vector3(-60.3478f, 4.7193f, -61.0687f);
            Main.MuteButton.transform.rotation = Quaternion.Euler(8.4964f, 31.8166f, 0f);

            if (Main.MuteButton.TryGetComponent<Collider>(out var collider))
                collider.isTrigger = true;

            Main.MuteButton.AddComponent<MuteTrigger>();

            if (Main.MuteButton.GetOrAddComponent<MeshRenderer>() != null)
            {
                Main.MuteButton.GetOrAddComponent<MeshRenderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                Main.MuteButton.GetOrAddComponent<MeshRenderer>().material.color = Color.white;
            }

            Main.IsLobbyMuted = false;
        }); 
        
        internal class MuteTrigger : GorillaPressableButton
        {
            public override void ButtonActivationWithHand(bool isLeftHand) => Main.IsLobbyMuted = !Main.IsLobbyMuted;
        }
    }
}