using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlightSource; // Sleep hier je Spotlight in
    public AudioSource clickSound; // Voor de 'scary sounds' en feedback

    void Update()
    {
        // F toets is standaard, maar je kan dit aanpassen
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        // Zet het licht aan of uit
        flashlightSource.enabled = !flashlightSource.enabled;

        // Speel een klikgeluid af (belangrijk voor immersie!)
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }
}