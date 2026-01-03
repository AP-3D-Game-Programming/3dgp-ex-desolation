using UnityEngine;
using UnityEngine.SceneManagement;

public class SimpleExitDoor : MonoBehaviour
{
    [Header("Where to go")]
    public int sceneIndex = 1;      // Which Scene to load
    public int targetSpawnID = 0;   // Which Spawn Point to land at (0, 1, 2...)
    
    [Header("Interaction")]
    public float range = 3f;

    [Header("UI Settings")]
    [Tooltip("Drag your Text object or Canvas Group here")]
    public GameObject promptUI; // <--- Drag your "[E] Enter" text here

    void Start()
    {
        // Make sure the text is hidden when the game starts
        if (promptUI != null) promptUI.SetActive(false);
    }

    void Update()
    {
        // Check distance
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        if (dist <= range)
        {
            // Player is CLOSE: Show the UI
            if (promptUI != null && !promptUI.activeSelf) 
            {
                promptUI.SetActive(true);
            }

            // Check for input
            if (Input.GetKeyDown(KeyCode.E))
            {
                EnterDoor();
            }
        }
        else
        {
            // Player is FAR: Hide the UI
            if (promptUI != null && promptUI.activeSelf) 
            {
                promptUI.SetActive(false);
            }
        }
    }

    void EnterDoor()
    {
        PlayerPrefs.SetInt("NextSpawnID", targetSpawnID);
        Debug.Log("Loading Scene " + sceneIndex + " at SpawnPoint " + targetSpawnID);
        SceneManager.LoadScene(sceneIndex);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}