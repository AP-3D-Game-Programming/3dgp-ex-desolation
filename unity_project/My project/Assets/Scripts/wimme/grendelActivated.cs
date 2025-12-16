using UnityEngine;

public class GrendelActivator : MonoBehaviour
{
    // Statische status van de grendel
    public static bool GrendelLosgehaald { get; private set; } = false;

    private Animator animator;
    private AudioSource audioSource; // NIEUW: Component voor de geluidsbron

    [Header("Hendel Instellingen")]
    public string activatieTrigger = "PullHandle";
    public KeyCode interactieToets = KeyCode.E;
    
    [Header("Geluid Instellingen")]
    // NIEUW: Sleep hier je geluidsbestand (bijv. een 'klik' of 'kraak') in
    public AudioClip activatieGeluidsClip; 

    private bool spelerBijHendel = false;

    void Start()
    {
        GrendelLosgehaald = false; // Reset de status bij start van de scene (belangrijk!)

        animator = GetComponent<Animator>();
        // NIEUW: Haal de AudioSource op (of voeg toe als deze ontbreekt)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false; // Speel niet automatisch af
        }
        
        if (animator == null)
        {
            Debug.LogError("GrendelActivator: Kan geen Animator component vinden. Animatie zal NIET werken!", this);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(interactieToets))
        {
            if (!GrendelLosgehaald && spelerBijHendel) 
            {
                ActiveerDeGrendel();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerBijHendel = true;
            Debug.Log("GrendelActivator: Speler is in de buurt. Interacteren is nu mogelijk.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerBijHendel = false;
            Debug.Log("GrendelActivator: Speler heeft de trigger zone verlaten.");
        }
    }

    void ActiveerDeGrendel()
    {
        // 1. ZET DE STATE OP WAAR
        GrendelLosgehaald = true;

        // 2. Start Animatie
        if (animator != null)
        {
            animator.SetTrigger(activatieTrigger);
        }
        
        // 3. SPEEL GELUID AF (NIEUW)
        if (activatieGeluidsClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(activatieGeluidsClip);
        }

        Debug.Log("GrendelActivator: De grendel is overgehaald! (Geluids- en Animatie gestart)");

        // De hendel is gebruikt, schakel dit script uit
        enabled = false;
    }
}