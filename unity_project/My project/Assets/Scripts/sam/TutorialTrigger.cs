using UnityEngine;
using TMPro;

public class TutorialTrigger : MonoBehaviour
{
    public GameObject tutorialTekst; // Sleep hier je "Press F" tekst-object in
    public bool destroyAfterUse = true; // Willen we dat de trigger verdwijnt na 1 keer?

    void Start()
    {
        // Zorg dat de tekst aan het begin onzichtbaar is
        if (tutorialTekst != null)
            tutorialTekst.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialTekst.SetActive(true);
            Debug.Log("De speler heeft hulp nodig met zijn zaklamp. Wat schattig.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            tutorialTekst.SetActive(false);

            if (destroyAfterUse)
            {
                // We hebben het verteld, nu moet hij het maar weten!
                Destroy(gameObject);
            }
        }
    }
}