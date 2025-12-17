using UnityEngine;

public class DoorActivator : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource; // NIEUW: Component voor geluid

    [Header("Deur Instellingen")]
    public string IsOpenParameterName = "IsOpen"; 
    public float sluitVertraging = 3.0f; 
    public KeyCode interactieToets = KeyCode.E;

    [Header("Geluiden")] // NIEUW: Variabelen voor de geluiden
    public AudioClip openGeluid;
    public AudioClip sluitGeluid;

    private bool isDeurGeopend = false;
    private bool spelerBijDeur = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // NIEUW: Haal de AudioSource op

        if (animator == null)
        {
            Debug.LogError("DoorActivator: Kan geen Animator component vinden op dit Deur GameObject.", this);
        }
        
        // Zorg ervoor dat de AudioSource aanwezig is voor geluidseffecten
        if (audioSource == null)
        {
            Debug.LogWarning("DoorActivator: Geen AudioSource component gevonden. Geluiden zullen NIET werken.", this);
            // Voeg optioneel een component toe als deze ontbreekt: audioSource = gameObject.AddComponent<AudioSource>();
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
                // CRUCIALE CHECK: Is de hendel overgehaald?
                if (GrendelActivator.GrendelLosgehaald)
                {
                    OpenDeur();
                }
                else
                {
                    // OPTIONEEL: Speel hier een "deur zit vast" geluid af
                    Debug.Log("DoorActivator: Kan de deur niet openen. De grendel is nog niet losgehaald.");
                }
            }
            // Handmatig sluiten
            else if (isDeurGeopend)
            {
                CancelInvoke("SluitDeur"); 
                SluitDeur();
            }
        }
    }

    // Trigger Enter/Exit logica blijft hetzelfde...

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
            // Sneller sluiten als de speler weggaat
            if (isDeurGeopend)
            {
                CancelInvoke("SluitDeur");
                Invoke("SluitDeur", sluitVertraging / 2f);
            }
        }
    }


    void OpenDeur()
    {
        isDeurGeopend = true;
        CancelInvoke("SluitDeur");
        
        if (animator != null)
        {
            animator.SetBool(IsOpenParameterName, true);
        }
        
        // GELUID: Speel het open geluid af
        if (audioSource != null && openGeluid != null)
        {
            audioSource.PlayOneShot(openGeluid);
        }

        Invoke("SluitDeur", sluitVertraging);
    }
    
    void SluitDeur()
    {
        if (isDeurGeopend)
        {
            if (animator != null)
            {
                animator.SetBool(IsOpenParameterName, false);
            }
            
            // GELUID: Speel het sluit geluid af
            if (audioSource != null && sluitGeluid != null)
            {
                audioSource.PlayOneShot(sluitGeluid);
            }

            isDeurGeopend = false;
        }
    }
}