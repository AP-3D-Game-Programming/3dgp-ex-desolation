using UnityEngine;

public class AutomatischeDeur : MonoBehaviour
{
    private Animator animator;
    private AudioSource audioSource; // Nieuwe variabele voor de geluidsbron

    [Header("Instellingen Animatie & Afstand")]
    public string openParameterNaam = "IsOpen"; 
    public float detectieAfstand = 3.0f; 
    public float sluitVertraging = 2.0f; 

    [Header("Geluiden")]
    // Sleep hier het geluidsbestand voor het openen in
    public AudioClip openGeluidsClip;
    // Sleep hier het geluidsbestand voor het sluiten in
    public AudioClip sluitGeluidsClip; 

    private Transform spelerTransform; 
    private bool isGeopend = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        // 1. Haal of voeg de AudioSource component toe
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Als er nog geen AudioSource is, voeg deze dan toe
            audioSource = gameObject.AddComponent<AudioSource>();
            // Optioneel: Stel in dat het geluid niet automatisch afspeelt
            audioSource.playOnAwake = false;
        }

        // Zoek de speler op
        GameObject speler = GameObject.FindGameObjectWithTag("Player");
        if (speler != null)
        {
            spelerTransform = speler.transform;
        }
        else
        {
            Debug.LogError("Player object niet gevonden! Zorg dat de speler de tag 'Player' heeft.");
        }
    }

    void Update()
    {
        if (spelerTransform == null)
        {
            return;
        }
        float afstandTotSpeler = Vector3.Distance(transform.position, spelerTransform.position);

        if (afstandTotSpeler <= detectieAfstand)
        {
            if (!isGeopend)
            {
                OpenDeur();
            }
        }
    }

    void OpenDeur()
    {
        isGeopend = true;
        animator.SetBool(openParameterNaam, true);

        // ** Geluid afspelen bij openen **
        if (openGeluidsClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(openGeluidsClip);
        }

        Invoke("SluitDeur", sluitVertraging);
    }

    void SluitDeur()
    {
        if (Vector3.Distance(transform.position, spelerTransform.position) > detectieAfstand)
        {
            isGeopend = false;
            animator.SetBool(openParameterNaam, false);

            // ** Geluid afspelen bij sluiten **
            if (sluitGeluidsClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(sluitGeluidsClip);
            }

            CancelInvoke("SluitDeur");
        }
        else
        {
            // Speler staat nog in de buurt, probeer later opnieuw te sluiten
            Invoke("SluitDeur", sluitVertraging / 2);
        }
    }
}