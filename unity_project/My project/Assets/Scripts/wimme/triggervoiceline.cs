using UnityEngine;

public class triggervoiceline : MonoBehaviour
{
    [Header("Audio Instellingen")]
    public AudioSource audioSource; 
    public AudioClip voiceLine;    // Het geluidsfragment (

    private bool isAfgespeeld = false;

    void OnTriggerEnter(Collider other)
    {
        // Check of de speler de trigger raakt en of het nog niet gespeeld is
        if (!isAfgespeeld && other.CompareTag("Player"))
        {
            if (audioSource != null && voiceLine != null)
            {
                isAfgespeeld = true; // Zorg dat het maar één keer gebeurt
                audioSource.PlayOneShot(voiceLine);
                Debug.Log("Voice line geactiveerd: " + voiceLine.name);
            }
        }
    }
}