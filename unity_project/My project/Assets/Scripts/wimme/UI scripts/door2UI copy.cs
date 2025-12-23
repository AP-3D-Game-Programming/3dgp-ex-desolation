// Bestand: StatusUITextProvider.cs
using UnityEngine;

public class Door3UI : MonoBehaviour, IInteractable
{
    [Header("Teksten")]
    [TextArea(1, 3)]
    public string ontgrendeldeTekst = "Unlocked";
    
    [TextArea(1, 3)]
    public string vergrendeldeTekst = "Locked";

    // De interface functie
    public string GetInteractionText()
    {
        // Controleer de status van de grendel
        if (KeyPadScript.CodeJuist)
        {
            return ontgrendeldeTekst;
        }
        else
        {
            return vergrendeldeTekst;
        }
    }
}