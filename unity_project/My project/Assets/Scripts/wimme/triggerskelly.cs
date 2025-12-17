using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [Header("Animatie Instellingen")]
    public Animator doelAnimator; // Sleep de Animator van het object hiernaartoe
    public string animatieTriggerNaam = "PlayAnimation"; // De naam van de 'Trigger' parameter in de Animator
    public bool eenmaligAfspelen = true; // Speelt de animatie maar één keer af

    private bool isGetriggerd = false; // Houdt bij of de animatie al is afgespeeld

    void OnTriggerEnter(Collider other)
    {
        // Controleer of het object dat de trigger raakt de "Player" is
        // Zorg dat je speler een tag "Player" heeft!
        if (other.CompareTag("Player"))
        {
            // Voorkom herhaling als het eenmalig moet zijn
            if (eenmaligAfspelen && isGetriggerd)
            {
                return; // Verlaat de functie als al afgespeeld
            }

            // Controleer of de Animator en de Trigger-naam correct zijn ingesteld
            if (doelAnimator != null)
            {
                doelAnimator.SetTrigger(animatieTriggerNaam);
                isGetriggerd = true; // Markeer als afgespeeld
                Debug.Log($"Animatie '{animatieTriggerNaam}' getriggerd door speler!");
            }
            else
            {
                Debug.LogWarning("Geen Animator toegewezen aan het TriggerAnimation script op " + gameObject.name);
            }
        }
    }

    // Optioneel: Voor debuggen, laat de trigger in de editor zien
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 1, 0, 0.3f); // Groen, semi-transparant
        Gizmos.DrawCube(transform.position, transform.localScale);
    }
}