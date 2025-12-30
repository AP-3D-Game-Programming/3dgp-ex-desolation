using UnityEngine;

public class StareAtPlayer : MonoBehaviour
{
    public Transform player; 
    
    // Add this variable to fix the "faceplant"
    // Try -90, 90, or 180 in the Inspector if it looks wrong
    public float rotationOffset = -90f; 

    void Update()
    {
        if (player == null) return;

        // 1. Determine where to look (ignoring height)
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);

        // 2. Look at the player
        transform.LookAt(targetPosition);

        // 3. Apply the correction to make it stand up
        // This adds extra rotation AFTER looking at the player
        transform.Rotate(rotationOffset, 0, 0);
    }
}