using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : MonoBehaviour
{
    //public GameObject heldItem;
    public Transform playerCamera; // Je Camera
    public Transform holdPos;      // Het punt waar het object moet hangen
    public GameObject Door; 
    public float pickUpRange = 4f;
    public float throwForce = 500f;

    public GameObject heldObj;
    private Rigidbody heldObjRb;
    private Collider heldObjCol;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (heldObj == null) TryPickUp();
            else 
            {
                Collider objectCollider = heldObj.GetComponent<Collider>();
                if (objectCollider.bounds.Intersects(Door.GetComponent<Collider>().bounds))
                {
                    Door.GetComponent<ChemicalLock>().AddChemical(heldObj.GetComponent<ChemicalItem>()); //Ophalen van ChemicalItem script
                    return;
                }
                else DropObject();
            };
        }

        // 2. INPUT: GOOIEN (Muisklik)
        if (heldObj != null && Input.GetMouseButtonDown(0))
        {
            ThrowObject();
        }
    }

    // LateUpdate zorgt voor soepele beweging zonder trillen
    void LateUpdate()
    {
        if (heldObj != null)
        {
            // HARD TELEPORTEREN
            // We gebruiken GEEN parenting. We dwingen het object gewoon naar de positie.
            heldObj.transform.position = holdPos.position;
        }
    }

    void TryPickUp()
    {
        RaycastHit hit;
        // Schiet straal vanuit camera
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, pickUpRange))
        {
            // Check of het object een Rigidbody heeft (anders kunnen we het niet pakken)
            if (hit.transform.GetComponent<Rigidbody>())
            {
                heldObj = hit.transform.gameObject;
                //heldItem = heldObj;
                Debug.Log("GEPAKT: " + heldObj.name);
                heldObjRb = heldObj.GetComponent<Rigidbody>();
                heldObjCol = heldObj.GetComponent<Collider>();

                // STAP 1: Physics UIT (Geen zwaartekracht)
                heldObjRb.isKinematic = true;

                // STAP 2: Collider UIT (SPOOK MODUS)
                // Dit is de magische fix. Als de collider uit staat, kan hij NERGENS
                // tegenaan botsen. Hij kan dus ook niet verdwijnen door physics glitches.
                if (heldObjCol != null) heldObjCol.isTrigger = true;
            }
        }
    }

    void DropObject()
    {
        // STAP 1: Physics weer AAN
        heldObjRb.isKinematic = false;
        heldObjRb.linearVelocity = Vector3.zero; // Reset snelheid (Unity 6)
        // heldObjRb.velocity = Vector3.zero; // Gebruik deze regel als je oude Unity hebt

        // STAP 2: Collider weer AAN
        if (heldObjCol != null) heldObjCol.isTrigger = false;

        // Reset variabelen
        heldObj = null;
        heldObjRb = null;
        heldObjCol = null;
    }

    void ThrowObject()
    {
        // Zelfde als Drop, maar met kracht
        heldObjRb.isKinematic = false;
        if (heldObjCol != null) heldObjCol.enabled = true;

        // Gooi in de richting waar de camera heen kijkt
        heldObjRb.AddForce(playerCamera.forward * throwForce);

        heldObj = null;
        heldObjRb = null;
        heldObjCol = null;
    }
}