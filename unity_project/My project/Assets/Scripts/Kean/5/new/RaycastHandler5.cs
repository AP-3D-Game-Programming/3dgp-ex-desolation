using TMPro;
using UnityEngine;

public class RaycastHandler5 : MonoBehaviour
{

    public Transform playerCamera;
    public float PickUpRange = 5f;
    public Canvas PlayerCanvas;
    public TextMeshProUGUI lookedAtItemText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, PickUpRange)) 
        {
            IInteractable5 interactor = hit.collider.GetComponent<IInteractable5>();

            if (interactor != null) 
            {
                PlayerCanvas.enabled = true;
                lookedAtItemText.text = hit.collider.gameObject.name;
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    interactor.Interact();
                }
            }
            else
            {
                PlayerCanvas.enabled = false;
            }
            
        }

        
           
    }
}
