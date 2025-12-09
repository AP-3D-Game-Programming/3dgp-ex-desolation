using UnityEngine;

public class FlashlightBob_OUD : MonoBehaviour
{
    [Header("Doel Tracking")]
    public Transform cameraTarget; 
    
    [Tooltip("Hoe snel de zaklamp de camera volgt (Hoger = sneller/minder lag).")]
    public float smoothness = 7.0f; 


    void Update()
    {
        if (cameraTarget == null)
        {
            Debug.LogError("Camera Target is niet ingesteld in de Inspector!");
            return;
        }

        Quaternion targetRotation = cameraTarget.rotation;

        transform.rotation = Quaternion.Slerp(
            transform.rotation, 
            targetRotation, 
            Time.deltaTime * smoothness
        );
    }
}