using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ObjectPickup : MonoBehaviour
{
    //public GameObject heldItem;
    public Transform playerCamera; // Je Camera
    public Transform holdPos;      // Het punt waar het object moet hangen
    public GameObject Door; 
    public float pickUpRange = 5f;
    public float throwForce = 500f;
    [SerializeField] private TextMeshProUGUI itemLookedAtText;
    public Canvas itemOverlayCanvas;
    [HideInInspector]
    public GameObject heldObj;
    private Rigidbody heldObjRb;
    private Collider heldObjCol;
    private bool canPickup = false;
    public string activeSceneName = "5_chemlab_puzzle";

    void Awake()
    {
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
        RaycastHit hit;
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpRange, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
        {
            ChemicalItem itemScript = hit.collider.GetComponent<ChemicalItem>();
            if (hit.collider.gameObject.name != "Note" && hit.collider.gameObject.name != "door" && itemScript != null && heldObj == null && hit.distance <= 1.5f)
            {
                itemOverlayCanvas.enabled = true;
                itemLookedAtText.text = itemScript.substanceName + " (E)";
                canPickup = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E ingedrukt op flask");
                if (canPickup && heldObj == null) 
                {
                    TryPickUp(hit);
                    itemOverlayCanvas.enabled = false;
                    //return;
                }
                    else 
                    {
                        Collider objectCollider = heldObj.GetComponent<Collider>();
                        if (objectCollider.bounds.Intersects(Door.GetComponent<Collider>().bounds))
                        {
                            Door.GetComponent<ChemicalLock>().AddChemical(heldObj.GetComponent<ChemicalItem>()); //Ophalen van ChemicalItem script
                            //return;
                        }
                        else DropObject();
                    };
                }
            
            if (itemScript == null && hit.collider.gameObject.name != "Note") 
            {
                itemOverlayCanvas.enabled = false;
            }
            if (hit.collider.gameObject.name == "door" && hit.distance <= 1.5f)
            {
            if (Door.GetComponent<ChemicalLock>().DoorUnlocked)
            {
            itemOverlayCanvas.enabled = true;
            itemLookedAtText.text = "Open (E)";
            }
            else if (!Door.GetComponent<ChemicalLock>().DoorUnlocked)
            {
                itemOverlayCanvas.enabled = true;
                itemLookedAtText.text = "";
            }
            }
            
            
        }
    }

    // LateUpdate zorgt voor soepele beweging zonder trillen
    void FixedUpdate()
    {
        if (heldObj != null)
        {
// Hoe harder je trekt, hoe strakker hij de hand volgt (bijv. 25f)
        float followSpeed = 25f; 

        // 1. Bereken de afstand tussen de hand en het object
        Vector3 direction = holdPos.position - heldObj.transform.position;

        // 2. Geef het object snelheid richting de hand in plaats van te teleporteren
        // Hierdoor stopt hij netjes als hij een muur raakt!
        heldObjRb.linearVelocity = direction * followSpeed;

        // 3. Zorg dat hij niet raar gaat tollen
        heldObjRb.angularVelocity = Vector3.zero;
        
        // Optioneel: Laat het object wel meedraaien met de hand
        heldObj.transform.rotation = holdPos.rotation;
        }
    }

    void TryPickUp(RaycastHit hit)
    {
        // Schiet straal vanuit camera
            // Check of het object een Rigidbody heeft (anders kunnen we het niet pakken)
            if (hit.transform.GetComponent<Rigidbody>() != null)
            {
                heldObj = hit.transform.gameObject;
                Debug.Log("GEPAKT: " + heldObj.name);
                heldObjRb = heldObj.GetComponent<Rigidbody>();
                heldObjCol = heldObj.GetComponent<Collider>();

                // STAP 1: Physics UIT (Geen zwaartekracht)
                heldObjRb.isKinematic = false;
                heldObjRb.useGravity = false;
                heldObjRb.linearDamping = 10f; 
                heldObjRb.angularDamping = 10f;
            }
    }

    void DropObject()
    {
        // STAP 1: Physics weer AAN
        heldObjRb.useGravity = true;
        //heldObjRb.linearVelocity = Vector3.zero;
        // heldObjRb.velocity = Vector3.zero; // Gebruik deze regel als je oude Unity hebt
        heldObjRb.linearDamping = 0.05f; 
        heldObjRb.angularDamping = 0.05f;

        // Reset variabelen
        heldObj = null;
        heldObjRb = null;
        heldObjCol = null;
    }
}