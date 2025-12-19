using UnityEngine;

public class LeverInteract : MonoBehaviour, IInteractable
{
    [Header("Referenties")]
    public Animator leverAnimator;     
    public SimpleKeypadDoor doelDeur; 
    public AudioSource audioSource;
    public AudioClip leverSound;

    [Header("Instellingen")]
    public string animatieTrigger = "Pull"; 
    public float interactieAfstand = 3f; // Zelfstandige afstandscheck
    private bool isUsed = false;
    private Transform spelerTransform;

    void Start()
    {
        // Zoek de speler op basis van de tag
        GameObject speler = GameObject.FindGameObjectWithTag("Player");
        if (speler != null)
        {
            spelerTransform = speler.transform;
        }
    }

    void Update()
    {
        // Alleen checken voor input als de hendel nog niet is gebruikt
        if (!isUsed && Input.GetKeyDown(KeyCode.E))
        {
            CheckInteractie();
        }
    }

    void CheckInteractie()
    {
        if (spelerTransform == null) return;

        // Bereken de afstand tussen de speler en de hendel
        float afstand = Vector3.Distance(transform.position, spelerTransform.position);

        if (afstand <= interactieAfstand)
        {
            // Optioneel: Check of de speler naar de hendel kijkt via een simpele kijkrichting-check
            Vector3 richtingNaarHendel = (transform.position - spelerTransform.position).normalized;
            float dotProduct = Vector3.Dot(Camera.main.transform.forward, richtingNaarHendel);

            // Als de speler ongeveer richting de hendel kijkt (dot product > 0.7)
            if (dotProduct > 0.7f)
            {
                Interact();
            }
        }
    }

    public string GetInteractionText()
    {
        if (isUsed) return "";
        return "Press [E]";
    }

    public void Interact()
    {
        if (!isUsed)
        {
            isUsed = true;
            PullLever();
        }
    }

    void PullLever()
    {
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger(animatieTrigger);
        }

        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }

        if (doelDeur != null)
        {
            doelDeur.OpenDeur();
        }

        Debug.Log("Hendel succesvol geactiveerd.");
    }
}