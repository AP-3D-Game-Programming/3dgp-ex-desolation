using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class NoteHandler : MonoBehaviour
{
    public Canvas NoteCanvas;
    public TextMeshProUGUI NoteTextMesh;
    public Transform playerCamera;
    public Canvas itemOverlayCanvas;
    public TextMeshProUGUI itemLookedAtText;
    public string NoteText = null;
    public First_Person_Movement playerMovementScript;
    private bool ReadingNote = false;

    public string activeSceneName = "5_chemlab_puzzle";
    
    void Awake()
    {
        // Controleer of de huidige scene de juiste is
        if (SceneManager.GetActiveScene().name != activeSceneName)
        {
            this.enabled = false;
        }
        else
        {
            this.enabled = true;
        }
    }

    void Update()
    {
        if (ReadingNote)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E ingedrukt op note");
                Debug.Log("Note gesloten");
                if (playerMovementScript != null ) playerMovementScript.enabled = true;
                CloseNote();
                ReadingNote = false; 
            }
            return;
        }

        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 4f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider.gameObject.name == "Note")
            {
                if (itemOverlayCanvas != null && !ReadingNote)
                {
                    itemOverlayCanvas.enabled = true;
                    itemLookedAtText.text = "Note";
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("E ingedrukt op note");
                    if (!ReadingNote)
                    {
                        Debug.Log("Note geopend");
                        if (playerMovementScript != null) playerMovementScript.enabled = false;
                        OpenNote();
                        ReadingNote = true; 
                    }
                    return;
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
