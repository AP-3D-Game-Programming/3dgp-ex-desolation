using UnityEngine;

public class SimpleKeypadDoor : MonoBehaviour
{
    private Animator animator;
    public string openParameter = "IsOpen"; // De naam van de bool in je Animator
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Deze functie wordt straks aangeroepen door het keypad
    public void OpenDeur()
    {
        if (animator != null)
        {
            animator.SetBool(openParameter, true);
            Debug.Log("Deur gaat open!");
        }
    }
}