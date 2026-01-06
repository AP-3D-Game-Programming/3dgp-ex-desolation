using UnityEngine;
using TMPro; 
using System.Collections; 

public class PickUpItem : MonoBehaviour
{
    [Header("Instellingen")]
    public PickUpKey deKey;
    public GameObject interactieTekst;
    public KeyCode interactieToets = KeyCode.E;

    [Header("Glow Instellingen")]
    public Renderer objectRenderer; 
    [ColorUsage(true, true)] public Color glowKleur;
    private Color standaardKleur = Color.black; 

    [Header("Audio")]
    public AudioSource spelerStemAudioSource;
    public AudioClip voiceLine;

    [Header("Subtitle Instellingen")]
    public string subtitleText;         
    public TextMeshProUGUI subtitleUI;  

    private bool isDichtbij = false;

    void Start()
    {
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
        float audioDuur = 0f;

        // Audio logica
        if (spelerStemAudioSource != null && voiceLine != null)
        {
            spelerStemAudioSource.clip = voiceLine;
            spelerStemAudioSource.Play();
            audioDuur = voiceLine.length;
        }

        if (deKey != null) deKey.hasWalkieTalkie = true;
        if (interactieTekst != null) interactieTekst.SetActive(false);

        // Verberg object en zet collider uit (zodat het lijkt alsof het weg is)
        if (objectRenderer != null) objectRenderer.enabled = false;
        if (GetComponent<Collider>() != null) GetComponent<Collider>().enabled = false;

        // Start de timer om tekst te tonen EN daarna het object te vernietigen
        StartCoroutine(AfhandelenEnVernietigen(audioDuur));
    }

    IEnumerator AfhandelenEnVernietigen(float delay)
    {
        // Toon tekst
        if (subtitleUI != null)
        {
            subtitleUI.text = subtitleText;
        }

        // Wacht tot audio klaar is
        yield return new WaitForSeconds(delay);

        // Wis tekst
        if (subtitleUI != null && subtitleUI.text == subtitleText)
        {
            subtitleUI.text = "";
        }

        // NU pas het object vernietigen, nadat de tekst weg is
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = true;
            if (interactieTekst != null) interactieTekst.SetActive(true);

            if (objectRenderer != null)
            {
                objectRenderer.material.SetColor("_EmissionColor", glowKleur);
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

            if (objectRenderer != null)
                objectRenderer.material.SetColor("_EmissionColor", standaardKleur);
        }
    }
}