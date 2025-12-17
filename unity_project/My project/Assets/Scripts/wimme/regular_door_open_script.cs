using UnityEngine;

public class RegularDoorOpenJumpscare : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource;
    
    [Header("Animatie & Input Instellingen")]
    public KeyCode interactieToets = KeyCode.E;
    public string openParameterNaam = "IsOpen";
    public string eersteKeerParameterNaam = "IsFirstTime";
    
    [Header("Trigger & Tijd Instellingen")]
    public float sluitVertraging = 1.5f; 
    
    [Header("Audio Instellingen")]
    public AudioClip openGeluidsClip;   // Krakend geluid bij openen
    public AudioClip sluitGeluidsClip;  // Krakend geluid bij normaal sluiten
    public AudioClip bamGeluidsClip;    // De harde klap (via Animation Event)
    
    private bool isGeopend = false;
    private bool isPlayerInTrigger = false;
    private bool isFirstTimeDoorUsed = true; 

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1f; 
        }

        // Zet de startwaarde voor de jumpscare in de animator
        animator.SetBool(eersteKeerParameterNaam, isFirstTimeDoorUsed);
    }

    void Update()
    {
        // CHECK: Alleen openen als de speler op E drukt EN in de trigger staat
        if (Input.GetKeyDown(interactieToets) && isPlayerInTrigger && !isGeopend)
        {
            OpenDeur();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            // Stop het automatisch sluiten als de speler weer terug de zone in loopt
            CancelInvoke("SluitDeurAutomatisch"); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            // Als de deur open is wanneer de speler wegloopt, start de sluit-timer
            if (isGeopend)
            {
                Invoke("SluitDeurAutomatisch", sluitVertraging);
            }
        }
    }

    public void OpenDeur()
    {
        isGeopend = true;
        animator.SetBool(openParameterNaam, true);
        
        if (openGeluidsClip != null)
        {
            audioSource.PlayOneShot(openGeluidsClip);
        }
    }

    void SluitDeurAutomatisch()
    {
        // Extra veiligheidscheck: sluit alleen als de speler echt buiten de trigger is
        if (!isPlayerInTrigger && isGeopend)
        {
            isGeopend = false;
            animator.SetBool(openParameterNaam, false);
            
            if (isFirstTimeDoorUsed)
            {
                // De Animator speelt nu de 'BAM' animatie af
                isFirstTimeDoorUsed = false;
                
                // Wacht heel even zodat de Animator de 'BAM' state pakt voordat we de bool op false zetten
                Invoke("DeactiveerFirstTime", 0.1f);
            }
            else
            {
                // Normale sluiting: speel het krakende geluid direct af
                if (sluitGeluidsClip != null)
                {
                    audioSource.PlayOneShot(sluitGeluidsClip);
                }
            }
        }
    }

    void DeactiveerFirstTime()
    {
        animator.SetBool(eersteKeerParameterNaam, false);
    }

    // ROEP DEZE AAN VIA EEN ANIMATION EVENT aan het einde van "door_close_bam"
    public void SpeelBamImpact()
    {
        if (bamGeluidsClip != null)
        {
            audioSource.PlayOneShot(bamGeluidsClip);
        }
    }
}