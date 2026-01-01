using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    [Header("Settings")]
    public EndingManager manager;   // Drag your GameManager object here
    public bool isLever;            // CHECK this for Lever, UNCHECK for Button

    private bool playerInRange = false;

    // 1. Detect when Player walks into the invisible box
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player entered interaction zone. Press E!");
            // Optional: You could turn on a "Press E" UI text here
        }
    }

    // 2. Detect when Player walks out
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // Optional: Turn off the "Press E" UI text here
        }
    }

    // 3. Listen for key press constantly
    private void Update()
    {
        // If we are in the zone AND we press E
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (isLever)
            {
                manager.TriggerReleaseEnding();
            }
            else
            {
                manager.TriggerBurnEnding();
            }

            // Disable this script so you can't trigger it twice
            this.enabled = false; 
        }
    }
}