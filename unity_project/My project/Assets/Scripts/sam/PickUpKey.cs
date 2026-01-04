using UnityEngine;
using TMPro; // Nodig voor de tekst

public class PickUpKey : MonoBehaviour
{
    [Header("Instellingen")]
    public DoorController deDeur;
    public GameObject interactieTekst; // Sleep hier je Text-object in
    public KeyCode interactieToets = KeyCode.E;
    public bool hasWalkieTalkie = false;

    [Header("Glow")]
    public Renderer objectRenderer;
    [ColorUsage(true, true)] public Color glowKleur = Color.yellow;
    private Color standaardKleur = Color.black; // Geen emission

    private bool isDichtbij = false;


    void Start()
    {
        // Zorg dat de glow uit staat bij het begin
        if (objectRenderer != null)
            objectRenderer.material.SetColor("_EmissionColor", standaardKleur);
    }

    void Update()
    {
        // Alleen als de speler dichtbij is EN op 'E' drukt
        if (isDichtbij && Input.GetKeyDown(interactieToets))
        {
            PakOp();
        }
    }

    void PakOp()
    {
        deDeur.hasKey = true;
        interactieTekst.SetActive(false); // Tekst wegdoen
        Debug.Log("Key opgepakt met 'E'. Goed gedaan, Einstein.");
        Destroy(gameObject);
    }

    // Als de speler de trigger binnenloopt
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasWalkieTalkie)
        {
            isDichtbij = true;
            interactieTekst.SetActive(true); // Tekst laten zien

            // ZET GLOW AAN
            if (objectRenderer != null)
            {
                objectRenderer.material.SetColor("_EmissionColor", glowKleur);
                // We moeten Unity vertellen dat het materiaal nu licht geeft
                objectRenderer.material.EnableKeyword("_EMISSION");
            }
        }
    }

    // Als de speler weer wegloopt zonder op 'E' te drukken
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasWalkieTalkie)
        {
            isDichtbij = false;
            interactieTekst.SetActive(false); // Tekst weer verbergen

            // ZET GLOW UIT
            if (objectRenderer != null)
                objectRenderer.material.SetColor("_EmissionColor", standaardKleur);
        }
    }
}