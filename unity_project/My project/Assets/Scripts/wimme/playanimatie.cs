using UnityEngine;

public class AnimationSleeper : MonoBehaviour
{
    private Animator animator;
    public AnimationClip deAnimatie;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (deAnimatie != null && animator != null)
        {
            // Speel de clip af op basis van de naam van het bestand
            animator.Play(deAnimatie.name);
        }
        else
        {
            Debug.LogWarning("Vergeet niet een animatie in het script te slepen op " + gameObject.name);
        }
    }
}