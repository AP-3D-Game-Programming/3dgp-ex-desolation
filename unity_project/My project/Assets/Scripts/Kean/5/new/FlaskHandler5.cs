using UnityEngine;

public class FlaskHandler5 : MonoBehaviour, IInteractable5
{
    public Transform holdPos;
    public float throwForce = 500f;
    public GameObject heldObj;
    private Rigidbody heldObjRb;
    private Collider heldObjCol;
    public Transform playerCamera;
    private bool isHeld = false;
    public void Interact()
    {
        
    }

    void TryPickUp(RaycastHit hit)
    {
        if (gameObject.GetComponent<Rigidbody>())
            {
                heldObj = gameObject;
                Debug.Log("GEPAKT: " + heldObj.name);
                heldObjRb = heldObj.GetComponent<Rigidbody>();
                heldObjCol = heldObj.GetComponent<Collider>();

                heldObjRb.isKinematic = true;

                if (heldObjCol != null) heldObjCol.isTrigger = true;
            }
    }

    void DropObject()
    {
        heldObjRb.isKinematic = false;
        heldObjRb.linearVelocity = Vector3.zero;

        if (heldObjCol != null) heldObjCol.isTrigger = false;

        heldObj = null;
        heldObjRb = null;
        heldObjCol = null;
    }

    void ThrowObject()
    {
        // Zelfde als Drop, maar met kracht
        heldObjRb.isKinematic = false;
        if (heldObjCol != null) 
        {
            heldObjCol.enabled = true;
            heldObjCol.isTrigger = false;
        }

        // Gooi in de richting waar de camera heen kijkt
        heldObjRb.AddForce(playerCamera.forward * throwForce);

        heldObj = null;
        heldObjRb = null;
        heldObjCol = null;
    }
}
