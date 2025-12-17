using UnityEngine;
using TMPro; 
using System.Collections; 

public class PlayerUIRaycaster : MonoBehaviour
{
    [Header("UI Elementen")]
    public Canvas itemOverlayCanvas;
    public TextMeshProUGUI itemLookedAtText; 

    [Header("Instellingen")]
    public Transform playerCamera; 
    public float interactieBereik = 3f; 
    
    // Verander dit in de Inspector naar 'Everything' of selecteer beide layers (Interactable & Draggable)
    public LayerMask detectieLayers; 

    [Header("Horror Stijl Instellingen")]
    public float typemachineSnelheid = 0.05f; 

    private string huidigeTekst = ""; 
    private Coroutine typeCoroutine; 

    void Update()
    {
        // 1. Standaard UI (Canvas inclusief Crosshair) uitzetten
        itemOverlayCanvas.enabled = false;

        RaycastHit hit;
        string nieuweTekst = "";
        bool toonUI = false;

        // 2. Raycast afvuren op de geselecteerde layers
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactieBereik, detectieLayers))
        {
            // We kijken naar iets op de juiste layer, dus de crosshair mag aan
            toonUI = true;
            itemOverlayCanvas.enabled = true;

            // 3. Check of het object ook tekst heeft (IInteractable interface)
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                nieuweTekst = interactable.GetInteractionText();
            }
            else
            {
                // Als het object wel op de layer zit (zoals een sleepbaar object) 
                // maar geen script heeft, tonen we alleen de crosshair (tekst wordt leeg)
                nieuweTekst = ""; 
            }
        }
        
        // 4. Typemachine Logica voor de tekst
        HandleTypemachine(toonUI, nieuweTekst);
    }

    void HandleTypemachine(bool isKijkend, string tekst)
    {
        if (isKijkend && tekst != "")
        {
            if (tekst != huidigeTekst)
            {
                if (typeCoroutine != null) StopCoroutine(typeCoroutine);
                huidigeTekst = tekst;
                typeCoroutine = StartCoroutine(TypeTekstCoroutine(tekst));
            }
        }
        else
        {
            if (typeCoroutine != null)
            {
                StopCoroutine(typeCoroutine);
                typeCoroutine = null;
            }
            itemLookedAtText.text = "";
            huidigeTekst = "";
        }
    }

    IEnumerator TypeTekstCoroutine(string tekstOmTeTonen)
    {
        itemLookedAtText.text = ""; 
        foreach (char letter in tekstOmTeTonen.ToCharArray())
        {
            itemLookedAtText.text += letter; 
            yield return new WaitForSeconds(typemachineSnelheid); 
        }
        typeCoroutine = null; 
    }
}