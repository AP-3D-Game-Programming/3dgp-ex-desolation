using UnityEngine;
using TMPro; 
using System.Collections; 

public class triggervoiceline : MonoBehaviour
{
    [Header("Audio Instellingen")]
    public AudioSource audioSource; 
    public AudioClip voiceLine;    // Het geluidsfragment (

    [Header("Subtitle Instellingen")]
    public string subtitleText;        // De tekst van de subtitle
    public TextMeshProUGUI subtitleUI; // Het tekstvak in de UI

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
                
                if (subtitleUI != null)
                {
                    subtitleUI.text = subtitleText; // Zet de tekst op het scherm
                    StartCoroutine(ClearSubtitle(voiceLine.length)); // Start timer om tekst weg te halen
                }

                Debug.Log("Voice line geactiveerd: " + voiceLine.name);
            }
        }
    }

    IEnumerator ClearSubtitle(float delay)
    {
        yield return new WaitForSeconds(delay); // Wacht tot de audio klaar is
        
        if (subtitleUI.text == subtitleText)
        {
            subtitleUI.text = ""; // Maak het tekstvak weer leeg
        }
    }
}