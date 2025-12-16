// Bestand: StatusUITextProvider.cs
using UnityEngine;

public class DoorUI : MonoBehaviour, IInteractable
{
    [Header("Teksten")]
    [TextArea(1, 3)]
    public string ontgrendeldeTekst = "Press [E]";
    
    [TextArea(1, 3)]
    public string vergrendeldeTekst = "Powered Down";

    // De interface functie
    public string GetInteractionText()
    {
        // Controleer de status van de grendel
        if (GrendelActivator.GrendelLosgehaald)
        {
            return ontgrendeldeTekst;
        }
        else
        {
            return vergrendeldeTekst;
        }
    }
}