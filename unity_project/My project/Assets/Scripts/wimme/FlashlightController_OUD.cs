using UnityEngine;

public class FlashlightController_OUD : MonoBehaviour
{
    [Header("Componenten")]
    public Light flashlightSource;
    public AudioSource clickSound;

    [Header("Batterij Instellingen")]
    public float batteryLife = 100f;
    public float drainRate = 5f;
    public float lowBatteryThreshold = 20f;

    private float initialIntensity;

    void Start()
    {
        if (flashlightSource != null)
        {
            initialIntensity = flashlightSource.intensity;
            flashlightSource.enabled = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }


        if (flashlightSource != null && flashlightSource.enabled)
        {

            if (batteryLife > 0)
            {
                batteryLife -= drainRate * Time.deltaTime;
                batteryLife = Mathf.Max(0, batteryLife);


                if (batteryLife <= lowBatteryThreshold)
                {
                    FlickerLight();
                }
            }

            if (batteryLife <= 0)
            {
                flashlightSource.enabled = false;
            }
        }
        else if (flashlightSource != null && !flashlightSource.enabled)
        {

            flashlightSource.intensity = initialIntensity;
        }
    }

    void ToggleFlashlight()
    {
        if (flashlightSource == null) return;

        if (!flashlightSource.enabled && batteryLife > 0)
        {
            flashlightSource.enabled = true;
        }
        else if (flashlightSource.enabled)
        {
            flashlightSource.enabled = false;
        }

        // Speel een klikgeluid af
        if (clickSound != null)
        {
            clickSound.Play();
        }
    }

    void FlickerLight()
    {

        float intensityFactor = Mathf.PerlinNoise(Time.time * 20, 0); // Snellere ruis

        flashlightSource.intensity = Mathf.Lerp(initialIntensity * 0.2f, initialIntensity, intensityFactor);

        if (Random.value < 0.05f)
        {
            flashlightSource.enabled = !flashlightSource.enabled;
        }
    }
}