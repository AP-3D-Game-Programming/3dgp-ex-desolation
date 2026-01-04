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

    private List<Transform> openedDoors = new List<Transform>();
    private List<Transform> animatingDoors = new List<Transform>();

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
                    playerCanvas.enabled = true;
                    interactText.text = "Interact (E)";
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                    Transform doorTransform = hit.collider.transform;
                    if (animatingDoors.Contains(doorTransform)) return;
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
                    playerCanvas.enabled = true;
                    interactText.text = "Pull (E)";
                }
                else
                {
                    playerCanvas.enabled = false;
                }
            
        }
        else
        {
            playerCanvas.enabled = false;
        }

    }

    IEnumerator AnimateDoorOpen(Transform doorTransform)
    {
        animatingDoors.Add(doorTransform);
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
        animatingDoors.Remove(doorTransform);
    }

    IEnumerator AnimateDoorClose(Transform doorTransform)
{
    animatingDoors.Add(doorTransform);
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
    animatingDoors.Remove(doorTransform);
}
}