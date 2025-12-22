using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleLeverPull : MonoBehaviour
{
    private string activeSceneName = "6_Maintenance_Room";
    public List<GameObject> lamp_lights = new List<GameObject>();
    public AudioSource ShortCircuitAudioSource;
    public AudioClip ShortCircuitSoundClip;
    private bool audioPlayed=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (SceneManager.GetActiveScene().name != activeSceneName)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GrendelActivator.GrendelLosgehaald)
        {
            if (!audioPlayed)
            {
                ShortCircuitAudioSource.PlayOneShot(ShortCircuitSoundClip);
                audioPlayed = true;
            }
            ShortCircuitAudioSource.PlayOneShot(ShortCircuitSoundClip);
            for (int i = 0; i < lamp_lights.Count; i++)
            {
                Destroy(lamp_lights[i]);
            }
        }
    }
}
