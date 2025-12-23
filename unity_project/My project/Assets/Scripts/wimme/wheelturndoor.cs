using UnityEngine;

public class ValveDoorOpener : MonoBehaviour, IInteractable
{
    [Header("Referenties")]
    public Transform deurTransform; // De deur die moet bewegen
    
    [Header("Bewegings Instellingen")]
    public Vector3 beweegRichting = new Vector3(0, 2.5f, 0); // Hoeveel de deur stijgt (bijv. 2.5 meter omhoog)
    public float draaiSnelheid = 20f; // Hoe snel het wiel draait in de hand van de speler
    public float benodigdeOmwentelingen = 5f; // Hoe vaak de speler moet 'klikken' om hem open te krijgen

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip kraakGeluid;

    private float huidigeVoortgang = 0f; // 0 is dicht, 1 is volledig open
    private Vector3 startPositieDeur;
    private Vector3 eindPositieDeur;

    void Start()
    {
        if (deurTransform != null)
        {
            startPositieDeur = deurTransform.localPosition;
            eindPositieDeur = startPositieDeur + beweegRichting;
        }
    }

    public string GetInteractionText()
    {
        if (huidigeVoortgang >= 1f) return "Mechanisme zit vast (Open)";
        return "Houd E ingedrukt om te draaien";
    }

    public void Interact()
    {
        // Deze functie wordt aangeroepen door je PlayerUIRaycaster
        if (huidigeVoortgang < 1f)
        {
            DraaiWiel();
        }
    }

    void DraaiWiel()
    {
        // 1. Voortgang verhogen
        float stap = 0.05f; // Hoeveel voortgang per klik
        huidigeVoortgang = Mathf.Clamp01(huidigeVoortgang + stap);

        // 2. Het wiel zelf visueel laten draaien
        transform.Rotate(Vector3.forward, draaiSnelheid);

        // 3. De deur verplaatsen op basis van de voortgang (Lerp)
        if (deurTransform != null)
        {
            deurTransform.localPosition = Vector3.Lerp(startPositieDeur, eindPositieDeur, huidigeVoortgang);
        }

        // 4. Geluid afspelen
        if (audioSource != null && kraakGeluid != null && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(kraakGeluid);
        }

        if (huidigeVoortgang >= 1f)
        {
            Debug.Log("Deur volledig geopend!");
        }
    }
} 