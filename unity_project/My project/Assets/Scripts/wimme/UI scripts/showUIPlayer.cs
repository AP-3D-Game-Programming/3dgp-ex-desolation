using UnityEngine;
using TMPro; 
using System.Collections; // Nodig voor Coroutines

public class PlayerUIRaycaster : MonoBehaviour
{
    [Header("UI Elementen")]
    // Sleep hier het Canvas element naartoe
    public Canvas itemOverlayCanvas;
    // Sleep hier het TextMeshPro Text component naartoe
    public TextMeshProUGUI itemLookedAtText; 

    [Header("Instellingen")]
    // Sleep hier de Camera van de speler naartoe
    public Transform playerCamera; 
    public float interactieBereik = 3f; 
    
    // Selecteer in de Inspector ALLEEN de Layer 'RaycastInteractable'
    public LayerMask interactableLayers; 

    [Header("Horror Stijl Instellingen")]
    // Hoe snel elke letter verschijnt
    public float typemachineSnelheid = 0.05f; 

    private string huidigeTekst = ""; 
    private Coroutine typeCoroutine; 

    void Update()
    {
        // 1. Standaard UI uitzetten
        itemOverlayCanvas.enabled = false;

        RaycastHit hit;
        string nieuweTekst = "";
        bool kijktNaarInteractable = false;

        // 2. Raycast afvuren, gebruikt LayerMask om alleen de kleine hitboxes te raken
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactieBereik, interactableLayers))
        {
            // Probeer de IInteractable interface te pakken van de geraakte collider
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                kijktNaarInteractable = true;
                itemOverlayCanvas.enabled = true;
                nieuweTekst = interactable.GetInteractionText();
            }
        }
        
        // 3. Typemachine Logica (Controleert of de tekst moet veranderen)
        if (kijktNaarInteractable)
        {
            if (nieuweTekst != huidigeTekst)
            {
                // Stop lopende animatie
                if (typeCoroutine != null)
                {
                    StopCoroutine(typeCoroutine);
                }
                
                huidigeTekst = nieuweTekst;
                // Start nieuwe animatie
                typeCoroutine = StartCoroutine(TypeTekstCoroutine(nieuweTekst));
            }
        }
        else // Kijkt niet naar een interactief object
        {
            // Ruim op: stop animatie en wis tekst
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
                typeCoroutine = null;
            }
            itemLookedAtText.text = "";
            huidigeTekst = "";
        }
    }

    // Coroutine voor het typemachine-effect
    IEnumerator TypeTekstCoroutine(string tekstOmTeTonen)
    {
        itemLookedAtText.text = ""; // Start met lege tekst
        
        foreach (char letter in tekstOmTeTonen.ToCharArray())
        {
            itemLookedAtText.text += letter; 
            yield return new WaitForSeconds(typemachineSnelheid); 
        }
        typeCoroutine = null; 
    }
}