using UnityEngine;

public class PickupSystem : MonoBehaviour
{
    [Header("Instellingen")]
    public LayerMask draggableLayer; // Selecteer hier je "Draggable" layer
    public float pickupRange = 3f;
    public float dragSpeed = 10f;

    [Header("Referenties")]
    private GameObject grabbedObject;
    private Rigidbody grabbedRb;
    private ConfigurableJoint joint;
    private GameObject targetPoint;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Linkermuisknop om te pakken
        {
            TryPickupObject();
        }

        if (Input.GetMouseButtonUp(0)) // Loslaten
        {
            DropObject();
        }
    }

    void FixedUpdate()
    {
        if (targetPoint != null)
        {
            // Beweeg het richtpunt naar de voorkant van de camera
            targetPoint.transform.position = transform.position + transform.forward * pickupRange;
        }
    }

    void TryPickupObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pickupRange, draggableLayer))
        {
            grabbedObject = hit.collider.gameObject;
            grabbedRb = grabbedObject.GetComponent<Rigidbody>();

            if (grabbedRb != null)
            {
                // Maak een tijdelijk richtpunt aan
                targetPoint = new GameObject("DragTarget");
                targetPoint.transform.position = hit.point;

                // Voeg een joint toe voor soepel slepen
                joint = grabbedObject.AddComponent<ConfigurableJoint>();
                joint.connectedBody = targetPoint.AddComponent<Rigidbody>();
                joint.connectedBody.isKinematic = true;

                // Configureer de joint voor "Drag" gedrag
                ConfigureJoint(joint);
            }
        }
    }

    void ConfigureJoint(ConfigurableJoint j)
    {
        j.xMotion = ConfigurableJointMotion.Locked;
        j.yMotion = ConfigurableJointMotion.Locked;
        j.zMotion = ConfigurableJointMotion.Locked;
        j.angularXMotion = ConfigurableJointMotion.Free;
        j.angularYMotion = ConfigurableJointMotion.Free;
        j.angularZMotion = ConfigurableJointMotion.Free;

        // Dit zorgt voor de "vering" tijdens het slepen
        SoftJointLimitSpring spring = new SoftJointLimitSpring();
        spring.spring = dragSpeed * 100;
        j.linearLimitSpring = spring;
    }

    void DropObject()
    {
        if (joint != null) Destroy(joint);
        if (targetPoint != null) Destroy(targetPoint);
        
        grabbedObject = null;
        grabbedRb = null;
    }
}