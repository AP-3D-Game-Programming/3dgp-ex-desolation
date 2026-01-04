using UnityEngine;
using TMPro;

public class PickUpItem : MonoBehaviour
{
    [Header("Instellingen")]
    public PickUpKey deKey;
    public GameObject interactieTekst;
    public KeyCode interactieToets = KeyCode.E;

    [Header("Audio Instellingen")]
    public AudioClip voiceLine;

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
        GameObject speler = GameObject.FindGameObjectWithTag("Player");

        if (speler != null)
        {
            AudioSource spelerAudio = speler.GetComponent<AudioSource>();

            if (spelerAudio != null && voiceLine != null)
            {
                // DE FIX: Check of er al iets speelt
                if (spelerAudio.isPlaying)
                {
                    Debug.Log("Er speelt al een voiceline. Ik wacht even met de rare shit...");
                    // Optioneel: spelerAudio.Stop(); // Gebruik dit als de nieuwe clip de oude moet afkappen
                }

                // Als er niks speelt (of als je de clip wilt forceren), spelen we het af
                if (!spelerAudio.isPlaying)
                {
                    spelerAudio.PlayOneShot(voiceLine);
                }
            }
        }

        // De rest van de logica blijft hetzelfde
        deKey.hasWalkieTalkie = true;
        if (interactieTekst != null) interactieTekst.SetActive(false);

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