using System.Runtime.CompilerServices;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public float openAngle = 90f; // Hoeveel graden moet hij draaien?
    public float speed = 1f;
    private Quaternion startRotation;
    private Quaternion endRotation;
    private bool DoorAlreadyOpened = false;
    private ObjectPickup ObjectPickupScript;
    public GameObject Player;
    private ChemicalLock ChemicalLockScript;
    private bool DoorCanBeOpened = false;

    private Collider DoorCollider;
    private Collider PlayerCollider;

    void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0, openAngle, 0) * startRotation;
        DoorCollider = gameObject.GetComponent<SphereCollider>();
        PlayerCollider = Player.GetComponent<Collider>();
        ChemicalLockScript = gameObject.GetComponent<ChemicalLock>();
        ObjectPickupScript = Player.GetComponent<ObjectPickup>();
    }

    void Update()
    {
        if (!DoorAlreadyOpened && ChemicalLockScript.DoorUnlocked)
        {
            if (ObjectPickupScript.heldObj == null && DoorCollider.bounds.Intersects(PlayerCollider.bounds) && Input.GetKeyDown(KeyCode.E))
            {
                DoorCanBeOpened = true;
                Debug.Log("Door opening");
            }
            if (DoorCanBeOpened) OpeningDoor();
        }
    }

    void OpeningDoor()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, endRotation, Time.deltaTime * speed);
        if (Quaternion.Angle(transform.rotation, endRotation) < 1.0f)
        {
            transform.rotation = endRotation;
            Debug.Log("Door Opened");
            DoorAlreadyOpened = true;
            DoorCollider.enabled = false;
        }
        
    }
}
