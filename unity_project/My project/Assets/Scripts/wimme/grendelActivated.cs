using UnityEngine;

public class GrendelActivator : MonoBehaviour
{
    // Statische status van de grendel
    public static bool GrendelLosgehaald { get; private set; } = false;

    private Animator animator;

    [Header("Hendel Instellingen")]
    public string activatieTrigger = "PullHandle";
    public KeyCode interactieToets = KeyCode.E;
    // De 'interactieAfstand' is technisch niet nodig omdat we nu een Trigger gebruiken.
    // Private bool om bij te houden of de speler binnen de trigger is
    private bool spelerBijHendel = false;

    void Start()
    {
        // 1. Probeer de Animator te pakken
        animator = GetComponent<Animator>();

        // DEBUG: Controleer of het script opstart en de Animator vindt
        if (animator == null)
        {
            Debug.LogError("GrendelActivator: Kan geen Animator component vinden op dit GameObject. Animatie zal NIET werken!", this);
        }
        else
        {
            Debug.Log("GrendelActivator: Script is gestart. Animator gevonden.");
        }
    }

    void Update()
    {
        // DEBUG: Controleer of de Update-loop überhaupt draait (zou continu moeten gebeuren)
        // Optioneel: Debug.Log("GrendelActivator: Update draait."); 

        // 2. Controleer alle drie de voorwaarden apart voor betere debugging
        if (Input.GetKeyDown(interactieToets))
        {
            Debug.Log($"GrendelActivator: Interactietoets ({interactieToets}) is ingedrukt!");

            if (GrendelLosgehaald)
            {
                Debug.Log("GrendelActivator: Fout, Grendel is al losgehaald. Doe niets.");
            }
            else if (!spelerBijHendel)
            {
                Debug.Log("GrendelActivator: Interactietoets ingedrukt, maar speler is NIET bij de hendel (spelerBijHendel is false).");
            }
            else // Alle voorwaarden zijn WAAR
            {
                ActiveerDeGrendel();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // DEBUG: Controleer of de Trigger-methode wordt geactiveerd
        Debug.Log($"GrendelActivator: OnTriggerEnter geactiveerd door: {other.gameObject.name} met Tag: {other.tag}");

        if (other.CompareTag("Player"))
        {
            spelerBijHendel = true;
            Debug.Log("GrendelActivator: **SUCCES!** Speler is in de buurt. Interacteren is nu mogelijk.");
            // UI Hint Log (indien nodig): "Druk op [E] om de hendel over te halen"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            spelerBijHendel = false;
            Debug.Log("GrendelActivator: Speler heeft de trigger zone verlaten.");
            // UI Hint Log (indien nodig): Hint verbergen
        }
    }

    void ActiveerDeGrendel()
    {
        // ZET DE STATE OP WAAR
        GrendelLosgehaald = true;

        // Start de animatie van de hendel
        if (animator != null)
        {
            // DEBUG: Bevestig dat de trigger-call wordt gemaakt
            Debug.Log($"GrendelActivator: Animator Trigger '{activatieTrigger}' wordt geactiveerd!");
            animator.SetTrigger(activatieTrigger);
        }

        Debug.Log("GrendelActivator: De grendel is overgehaald! (Animatie gestart, script uitgeschakeld)");

        // De hendel is gebruikt, schakel dit script uit om dubbele acties te voorkomen
        enabled = false;
    }
}