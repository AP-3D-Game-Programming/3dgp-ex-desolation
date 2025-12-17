using UnityEngine;
using UnityEngine.SceneManagement;
public class SwitchScene : MonoBehaviour
{
private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Huidige index ophalen (bijv. Scene 0)
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // De volgende laden (Scene 0 + 1 = Scene 1)
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
