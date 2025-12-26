using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleExitDoor : MonoBehaviour
{
    [Header("Where to go")]
    public int sceneIndex = 1;      // Which Scene to load
    public int targetSpawnID = 0;   // Which Spawn Point to land at (0, 1, 2...)
    
    [Header("Interaction")]
    public float range = 3f;

    void Update()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= range)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // SAVE the Spawn ID so the next scene can read it
                PlayerPrefs.SetInt("NextSpawnID", targetSpawnID);
                
                Debug.Log("Loading Scene " + sceneIndex + " at SpawnPoint " + targetSpawnID);
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}