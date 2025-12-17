// Bestand: SimpleUITextProvider.cs
using UnityEngine;

public class BasicUI : MonoBehaviour, IInteractable
{
    // De tekst die getoond moet worden (bijv. "Druk E om op te rapen", of "Druk E om te openen")
    [TextArea(1, 3)]
    public string interactionText = "Press [E]";

    // De interface functie
    public string GetInteractionText()
    {
        return interactionText;
    }
}
