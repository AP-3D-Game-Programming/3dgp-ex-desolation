using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Instellingen")]
    public Animator doorAnimator;    // Sleep hier je Animator in
    public AudioSource doorAudio;     // Sleep hier je AudioSource in

    [Header("Status")]
    public bool hasWalkieTalkie = false; // Dit wordt 'true' via het andere script

    // Deze functie wordt aangeroepen door de Trigger op de DEUR
    void OnTriggerEnter(Collider other)
    {
        // Checken we of het de speler is?
        if (other.CompareTag("Player"))
        {
            if (hasWalkieTalkie)
            {
                OpenDeur();
            }
            else
            {
                Debug.Log("De deur zit op slot. Misschien moet je die walkie-talkie eens gaan zoeken, pannenkoek!");
                // Hier zou je eventueel een 'zit-op-slot' geluidje kunnen afspelen
            }
        }
    }

    void OpenDeur()
    {
        // Start de animatie (zorg dat de trigger in je Animator 'Open' heet!)
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }

        // Speel het geluid af
        if (doorAudio != null && !doorAudio.isPlaying)
        {
            doorAudio.Play();
        }

        Debug.Log("Kijk nou, de deur gaat open. Eindelijk progressie!");
    }
}
