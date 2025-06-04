using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    public OculusInputHandler rightHandInput;
    public Gun gun;
    public Light flashlight;

    void Start()
    {
        if (rightHandInput != null)
        {
            rightHandInput.OnTriggerButtonJustPressed += gun.Shoot;
            rightHandInput.OnPrimaryButtonJustPressed += ToggleFlashlight;
        }
    }

    void ToggleFlashlight()
    {
        if (flashlight != null)
            flashlight.enabled = !flashlight.enabled;
    }
}