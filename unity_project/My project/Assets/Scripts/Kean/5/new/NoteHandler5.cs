using UnityEngine;
using TMPro;

public class NoteHandler5 : MonoBehaviour, IInteractable5
{
    private bool ReadingNote = false;
    public Canvas NoteCanvas;
    public TextMeshProUGUI NoteTextMesh;
    public Canvas itemOverlayCanvas;
    public TextMeshProUGUI itemLookedAtText;
    public string NoteText = null;
    public First_Person_Movement playerMovementScript;
    public void Interact()
    {
        if (ReadingNote)
        {
            if (playerMovementScript != null ) playerMovementScript.enabled = true;
            CloseNote();
            ReadingNote = false; 
            return;
        }
        else if (!ReadingNote)
        {
            if (playerMovementScript != null) playerMovementScript.enabled = false;
            OpenNote();
            ReadingNote = true;
            return;
        }        
    }
    public void OpenNote()
    {
        if (itemOverlayCanvas != null)
        {
            itemOverlayCanvas.enabled = false;
        }
        NoteCanvas.enabled = true;
        if (NoteText != null)
        {
            NoteTextMesh.text = NoteText;
        }
        Time.timeScale = 0f;
    }
    public void CloseNote() 
    {
        NoteCanvas.enabled = false;
        Time.timeScale = 1f;
    }
     
}
