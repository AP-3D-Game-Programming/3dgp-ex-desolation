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
    // Sleep hier het geluid voor het OPENEN in
    public AudioClip openGeluidsClip;
    // Sleep hier het geluid voor het NORMALE SLUITEN in
    public AudioClip sluitGeluidsClip;
    // Sleep hier het geluid voor de JUMP SCARE (BAM!) in
    public AudioClip bamGeluidsClip; 
    
    private bool isGeopend = false;
    private bool isPlayerInTrigger = false;
    private bool isFirstTimeDoorUsed = true; // Start op TRUE voor de jump scare

    void Start()
    {
        animator = GetComponent<Animator>();
        
        // Haal of voeg de AudioSource component toe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            // Zorg dat het geluid in de 3D-wereld klinkt (optioneel)
            audioSource.spatialBlend = 1f; 
        }

        // Zorg dat de Animator de 'IsFirstTime' bool kent
        animator.SetBool(eersteKeerParameterNaam, isFirstTimeDoorUsed);
    }

    void Update()
    {
        // ... (De interactie met de 'E' toets via de Raycaster)
        // Voor nu, een placeholder om de deur te openen als 'E' wordt ingedrukt
        if (Input.GetKeyDown(interactieToets) && !isGeopend)
        {
            // Je moet hier nog controleren of de speler naar de deur kijkt (via de Raycaster)
            OpenDeur();
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            CancelInvoke("SluitDeurAutomatisch"); 
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            if (isGeopend)
            {
                Invoke("SluitDeurAutomatisch", sluitVertraging);
            }
        }
    }

    void OpenDeur()
    {
        isGeopend = true;
        animator.SetBool(openParameterNaam, true);
        
        // *** GELUID: DEUR OPENEN ***
        if (openGeluidsClip != null)
        {
            audioSource.PlayOneShot(openGeluidsClip);
        }
    }

    void SluitDeurAutomatisch()
    {
        if (!isPlayerInTrigger && isGeopend)
        {
            isGeopend = false;
            animator.SetBool(openParameterNaam, false);
            
            if (isFirstTimeDoorUsed)
            {
                // De Animator start de 'BAM' animatie
                isFirstTimeDoorUsed = false;
                animator.SetBool(eersteKeerParameterNaam, false); 
                
                // *** GELUID: JUMP SCARE BAM! ***
                if (bamGeluidsClip != null)
                {
                    audioSource.PlayOneShot(bamGeluidsClip);
                }
            }
            else
            {
                // De Animator start de normale sluit animatie
                
                // *** GELUID: NORMAAL SLUITEN ***
                if (sluitGeluidsClip != null)
                {
                    audioSource.PlayOneShot(sluitGeluidsClip);
                }
            }
        }
        else if (isPlayerInTrigger)
        {
            CancelInvoke("SluitDeurAutomatisch");
        }
    }
}