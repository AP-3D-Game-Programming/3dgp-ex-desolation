using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    [Header("Instellingen")]
    public PickUpKey deKey;
    public GameObject interactieTekst;
    public KeyCode interactieToets = KeyCode.E;

    [Header("Glow Instellingen")]
    public Renderer objectRenderer; // Sleep hier de Mesh Renderer van de walkie-talkie in
    [ColorUsage(true, true)] public Color glowKleur;
    private Color standaardKleur = Color.black; // Geen emission

    [Header("Audio")]
    public AudioSource spelerStemAudioSource;
    public AudioClip voiceLine;

    private bool isDichtbij = false;

    void Start()
    {
        // Zorg dat de glow uit staat bij het begin
        if (objectRenderer != null)
            objectRenderer.material.SetColor("_EmissionColor", standaardKleur);
    }

    void Update()
    {
        if (isDichtbij && Input.GetKeyDown(interactieToets))
        {
            PakOp();
        }
    }

    void PakOp()
    {
        // Audio logica (die hadden we al)
        if (spelerStemAudioSource != null && voiceLine != null)
        {
            spelerStemAudioSource.clip = voiceLine;
            spelerStemAudioSource.Play();
        }

        if (deKey != null) deKey.hasWalkieTalkie = true;
        if (interactieTekst != null) interactieTekst.SetActive(false);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = true;
            if (interactieTekst != null) interactieTekst.SetActive(true);

            // ZET GLOW AAN
            if (objectRenderer != null)
            {
                objectRenderer.material.SetColor("_EmissionColor", glowKleur);
                // We moeten Unity vertellen dat het materiaal nu licht geeft
                objectRenderer.material.EnableKeyword("_EMISSION");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = false;
            if (interactieTekst != null) interactieTekst.SetActive(false);

            // ZET GLOW UIT
            if (objectRenderer != null)
                objectRenderer.material.SetColor("_EmissionColor", standaardKleur);
        }
    }
}