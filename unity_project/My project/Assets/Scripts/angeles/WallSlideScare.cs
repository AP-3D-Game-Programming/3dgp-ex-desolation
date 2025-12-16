using UnityEngine;
using System.Collections;

public class WallSlideScare : MonoBehaviour
{
    [Header("Assign in Inspector")]
    [Tooltip("The image or object you want to move")]
    public GameObject scaryObject;

    [Tooltip("An empty GameObject placed inside the wall where the image should end up")]
    public Transform targetDestination; 

    [Header("Settings")]
    public float slideSpeed = 5.0f;
    [Tooltip("If true, the scare only happens once.")]
    public bool playOnce = true;

    [Header("Audio (Optional)")]
    public AudioSource soundEffect;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering is the Player
        // Make sure your player object has the tag "Player"
        if (other.CompareTag("Player")) 
        {
            if (playOnce && hasTriggered) return;

            StartCoroutine(SlideObject());
            hasTriggered = true;
        }
    }

    private IEnumerator SlideObject()
    {
        if (soundEffect != null) soundEffect.Play();

        // Continue moving until the object reaches the target
        while (Vector3.Distance(scaryObject.transform.position, targetDestination.position) > 0.01f)
        {
            // Move our object towards the destination
            scaryObject.transform.position = Vector3.MoveTowards(
                scaryObject.transform.position, 
                targetDestination.position, 
                slideSpeed * Time.deltaTime
            );

            // Wait for the next frame
            yield return null; 
        }

        // Snap to exact position at the end to prevent micro-jitter
        scaryObject.transform.position = targetDestination.position;
    }
}