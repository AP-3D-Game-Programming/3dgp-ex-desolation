using TMPro;
using UnityEngine;

public class NoteHandlerWimme : MonoBehaviour
{
    public Canvas NoteCanvas;
    public TextMeshProUGUI NoteTextMesh;
    public Transform playerCamera;
    public Canvas itemOverlayCanvas;
    public TextMeshProUGUI itemLookedAtText;
    public string NoteText = null;
    public First_Person_Movement playerMovementScript;
    private bool ReadingNote = false;
    void Update()
    {
        if (ReadingNote)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Note gesloten");
                if (playerMovementScript != null ) playerMovementScript.enabled = true;
                CloseNote();
                ReadingNote = false; 
            }
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 4f))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (!ReadingNote)
                    {
                        Debug.Log("Note geopend");
                        if (playerMovementScript != null )playerMovementScript.enabled = false;
                        OpenNote();
                        ReadingNote = true; 
                    }
                    return;
                }
            }
            else
            {
                if (itemOverlayCanvas != null)
                {
                    itemOverlayCanvas.enabled = false;
                }
                
            }
        }

        

    }
    public void CloseNote() 
    {
        NoteCanvas.enabled = false;
        Time.timeScale = 1f;
        
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
}
