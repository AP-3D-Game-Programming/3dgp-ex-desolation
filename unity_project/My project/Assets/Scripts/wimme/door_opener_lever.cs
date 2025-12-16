using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    private Animator animator;

    [Header("Deur Instellingen")]
    // De naam van de Bool parameter in de Animator (NIEUW!)
    // True = Open, False = Gesloten
    public string IsOpenParameterName = "IsOpen"; 
    
    // De tijd in seconden voordat de deur automatisch sluit
    public float sluitVertraging = 3.0f; 

    // De toets om de deur te openen
    public KeyCode interactieToets = KeyCode.E;

    // Houdt de huidige staat van de deur bij
    private bool isDeurGeopend = false;
    private bool spelerBijDeur = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("DoorActivator: Kan geen Animator component vinden op dit Deur GameObject.", this);
        }
    }

    void Update()
    {
        // Alleen interacteren als de speler bij de deur is
        if (spelerBijDeur && Input.GetKeyDown(interactieToets))
        {
            // Deur is gesloten en moet geopend worden
            if (!isDeurGeopend)
            {
                Debug.Log("DoorActivator: Speler probeert deur te openen. Controleer grendelstatus...");

                // CRUCIALE CHECK: Is de hendel overgehaald?
                if (GrendelActivator.GrendelLosgehaald)
                {
                    OpenDeur();
                }
                else
                {
                    Debug.Log("DoorActivator: Kan de deur niet openen. De grendel is nog niet losgehaald.");
                }
            }
            // Optionele functie: Als de deur al open is, sluit deze direct met E
            else if (isDeurGeopend)
            {
                 // Zorg ervoor dat de automatische sluiting wordt geannuleerd als we handmatig sluiten
                CancelInvoke("SluitDeur"); 
                SluitDeur();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerBijDeur = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerBijDeur = false;
            // Als de speler weggaat terwijl de deur open is, sluit deze dan sneller
            if (isDeurGeopend)
            {
                // Zorg dat er slechts één sluitcommando tegelijk loopt
                CancelInvoke("SluitDeur");
                Invoke("SluitDeur", sluitVertraging / 2f); // Sluit sneller als de speler weggaat
            }
        }
    }

    void OpenDeur()
    {
        isDeurGeopend = true;
        
        // Annuleer eventuele geplande sluitingen
        CancelInvoke("SluitDeur");
        
        // Zet de Bool parameter op TRUE (deur gaat open)
        if (animator != null)
        {
            Debug.Log($"DoorActivator: Deur wordt geopend! Bool '{IsOpenParameterName}' wordt op TRUE gezet.");
            animator.SetBool(IsOpenParameterName, true);
        }

        // Plan het automatisch sluiten in na de volledige vertraging
        Invoke("SluitDeur", sluitVertraging);
    }
    
    void SluitDeur()
    {
        // We sluiten alleen als de deur nog open is
        if (isDeurGeopend)
        {
            if (animator != null)
            {
                // Zet de Bool parameter op FALSE (deur gaat dicht)
                Debug.Log($"DoorActivator: Deur sluit automatisch! Bool '{IsOpenParameterName}' wordt op FALSE gezet.");
                animator.SetBool(IsOpenParameterName, false);
            }
            
            // Zet de staat terug op gesloten
            isDeurGeopend = false;
        }
    }
}