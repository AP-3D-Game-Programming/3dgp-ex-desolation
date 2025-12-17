using UnityEngine;

public class lightOnLever : MonoBehaviour
{

    private Animator animator;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); 

    }

    // Update is called once per frame
    void Update()
    {
        if (GrendelActivator.GrendelLosgehaald)
        {
            animator.SetBool("light_on", true);
        }
    }
}
