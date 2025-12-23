using UnityEngine;
using TMPro; // Nodig voor de tekst

public class PickUpItem : MonoBehaviour
{
    [Header("Instellingen")]
    public PickUpKey deKey;
    public GameObject interactieTekst; // Sleep hier je Text-object in
    public KeyCode interactieToets = KeyCode.E;

    private bool isDichtbij = false;

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
        deKey.hasWalkieTalkie = true;
        interactieTekst.SetActive(false); // Tekst wegdoen
        Debug.Log("Walkie-talkie opgepakt met 'E'. Goed gedaan, Einstein.");
        Destroy(gameObject);
    }

    // Als de speler de trigger binnenloopt
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = true;
            interactieTekst.SetActive(true); // Tekst laten zien
        }
    }

    // Als de speler weer wegloopt zonder op 'E' te drukken
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isDichtbij = false;
            interactieTekst.SetActive(false); // Tekst weer verbergen
        }
    }
}
