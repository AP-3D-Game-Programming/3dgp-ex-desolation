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
    
    public LayerMask detectieLayers; 

    [Header("Horror Stijl Instellingen")]
    public float typemachineSnelheid = 0.05f; 

    private string huidigeTekst = ""; 
    private Coroutine typeCoroutine; 

    void Update()
    {
        itemOverlayCanvas.enabled = false;

        RaycastHit hit;
        string nieuweTekst = "";
        bool toonUI = false;

        // TEKEN EEN STRAAL IN DE SCENE VIEW (Alleen zichtbaar in de editor tijdens Play)
        Debug.DrawRay(playerCamera.position, playerCamera.forward * interactieBereik, Color.red);

        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactieBereik, detectieLayers))
        {
            // DEBUG: Laat zien welk object geraakt wordt en op welke layer het zit
            Debug.Log($"Raycast raakt: {hit.collider.gameObject.name} op Layer: {LayerMask.LayerToName(hit.collider.gameObject.layer)}");

            toonUI = true;
            itemOverlayCanvas.enabled = true;

            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                nieuweTekst = interactable.GetInteractionText();
                // DEBUG: Bevestig dat de interface gevonden is
                Debug.Log("IInteractable component gevonden!");
            }
            else
            {
                nieuweTekst = ""; 
                // DEBUG: Waarschuwing als er geen script op zit
                Debug.Log("Object geraakt, maar geen IInteractable script gevonden.");
            }
        }
        
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