using UnityEngine;

public class VoiceLineManager : MonoBehaviour
{
    // Sleep hier in de Inspector het geluidsfragment naartoe
    public AudioClip toPlayClip;
    public AudioSource toPlaySource; 
    private bool scriptAdded = false;

    void Update()
    {
        // 1. Check of de gebeurtenis heeft plaatsgevonden
        if (GrendelActivator.GrendelLosgehaald && !scriptAdded)
        {
            triggervoiceline newScript = gameObject.AddComponent<triggervoiceline>();

            newScript.voiceLine = toPlayClip;
            newScript.audioSource = toPlaySource;

            scriptAdded = true;
        }
    }
}