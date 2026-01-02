using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    [Header("Instellingen")]
    public PickUpKey deKey; // Je referentie naar het sleutel/deur-script
    public GameObject interactieTekst;
    public KeyCode interactieToets = KeyCode.E;

    [Header("Audio Instellingen")]
    public AudioClip voiceLine; // Sleep hier je geluidsbestand (.mp3/.wav) in

    private bool isDichtbij = false;

    void Update()
    {
        if (isDichtbij && Input.GetKeyDown(interactieToets))
        {
            PakOp();
        }
    }

    void PakOp()
    {
        // 1. Zoek de speler op basis van de Tag "Player"
        GameObject speler = GameObject.FindGameObjectWithTag("Player");

        if (speler != null)
        {
            // 2. Haal de AudioSource van de speler op
            AudioSource spelerAudio = speler.GetComponent<AudioSource>();

            if (spelerAudio != null && voiceLine != null)
            {
                // 3. Speel de voiceline af
                spelerAudio.PlayOneShot(voiceLine);
                Debug.Log("Voiceline wordt afgespeeld via de speler. Luister je wel?");
            }
            else if (spelerAudio == null)
            {
                Debug.LogWarning("Ik vind de speler wel, maar er zit geen AudioSource op! Doe dat eens even!");
            }
        }
        else
        {
            Debug.LogError("Ik kan geen object vinden met de tag 'Player'. Heb je die tag wel ingesteld?");
        }

        // De rest van je logica
        deKey.hasWalkieTalkie = true;
        if (interactieTekst != null) interactieTekst.SetActive(false);

        Debug.Log("Walkie-talkie opgepakt. Eindelijk progressie.");
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = true;
            if (interactieTekst != null) interactieTekst.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = false;
            if (interactieTekst != null) interactieTekst.SetActive(false);
        }
    }
}