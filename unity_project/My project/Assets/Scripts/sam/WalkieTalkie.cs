using UnityEngine;

public class WalkieTalkie : MonoBehaviour
{
    // We hebben een referentie nodig naar het script op de deur
    public DoorOpener deDeur;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // We vertellen de deur dat we de walkie-talkie hebben!
            deDeur.hasWalkieTalkie = true;

            Debug.Log("Je hebt de walkie-talkie opgepakt. Eindelijk doe je iets goed.");

            // Verwijder de walkie-talkie uit de wereld
            Destroy(gameObject);
        }
    }
}
