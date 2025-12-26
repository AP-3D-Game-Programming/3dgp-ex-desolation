using UnityEngine;

public class PlayerEntry : MonoBehaviour
{
    void Start()
    {
        // 1. Read the ID we saved in the last scene (defaults to 0 if none found)
        int spawnID = PlayerPrefs.GetInt("NextSpawnID", 0);

        // 2. Find the object named "SpawnPoint_X"
        string pointName = "SpawnPoint_" + spawnID;
        GameObject spawnPoint = GameObject.Find(pointName);

        // 3. Teleport!
        if (spawnPoint != null)
        {
            // We need to disable CharacterController briefly to teleport (if you use one)
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            // Move the player
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;

            // Re-enable controller
            if (cc != null) cc.enabled = true;

            Debug.Log("Teleported to: " + pointName);
        }
        else
        {
            Debug.LogWarning("Could not find a spawn point named: " + pointName);
        }
    }
}