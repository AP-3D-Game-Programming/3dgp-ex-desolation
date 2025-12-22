using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class RayCastHandler : MonoBehaviour
{
    public float interactRange = 2f;
    public float openSpeed = 1f;
    public Canvas playerCanvas;
    public TextMeshProUGUI interactText;
    public string activeSceneName = "6_Maintenance_Room";
    public GameObject Player;
    private Collider playerCollider;
    public GameObject lever;
    private Collider leverCollider;



    // We gebruiken een List om bij te houden welke deuren al open zijn
    private List<Transform> openedDoors = new List<Transform>();
    private List<Transform> closedDoors = new List<Transform>();

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

    void Start()
    {
        playerCollider = Player.GetComponent<Collider>();
        leverCollider = lever.GetComponent<Collider>();
    }


    void Update()
    {
        RaycastHit hit;
        bool hitSomething = Physics.Raycast(transform.position, transform.forward, out hit, interactRange);
        if (hitSomething)
        {
                if (hit.collider.CompareTag("Door"))
                {
                    ShowUI(true, "Interact (E)");
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                    Transform doorTransform = hit.collider.transform;
                    if (!openedDoors.Contains(doorTransform))
                    {
                        StartCoroutine(AnimateDoorOpen(doorTransform)); 
                    }
                    else if (openedDoors.Contains(doorTransform))
                    {
                        StartCoroutine(AnimateDoorClose(doorTransform));
                    }
                    }
                }
                else if (playerCollider.bounds.Intersects(leverCollider.bounds) && !GrendelActivator.GrendelLosgehaald)
                {
                    ShowUI(true, "Pull (E)");
                }
                else
                {
                    ShowUI(false);
                }
            
        }
        else
        {
            ShowUI(false);

        }
    }
    // Hulpmethode om geflikker te voorkomen: check eerst of de staat wel moet veranderen
    void ShowUI(bool state, string text = "")
    {
        if (playerCanvas.enabled != state) 
        {
            playerCanvas.enabled = state;
        }
        if (state) 
        {
            interactText.text = text;
        }
    }

    IEnumerator AnimateDoorOpen(Transform doorTransform)
    {
        openedDoors.Add(doorTransform);
        
        Quaternion startRot = doorTransform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 90f, 0) * startRot;
        
        float progress = 0;
        while (progress < 1)
        {
            doorTransform.rotation = Quaternion.Slerp(startRot, endRot, progress);
            progress += Time.deltaTime * openSpeed;
            yield return null;
        }

        doorTransform.rotation = endRot;
    }

    IEnumerator AnimateDoorClose(Transform doorTransform)
{
    openedDoors.Remove(doorTransform);
    Quaternion startRot = doorTransform.rotation;
    // We draaien -90 graden op de Y-as om de beweging om te keren
    Quaternion endRot = Quaternion.Euler(0, -90f, 0) * startRot;
    
    float progress = 0;
    while (progress < 1)
    {
        doorTransform.rotation = Quaternion.Slerp(startRot, endRot, progress);
        // We gebruiken hier openSpeed, maar je zou ook een aparte closeSpeed kunnen maken
        progress += Time.deltaTime * openSpeed; 
        yield return null;
    }

    doorTransform.rotation = endRot;
}
}