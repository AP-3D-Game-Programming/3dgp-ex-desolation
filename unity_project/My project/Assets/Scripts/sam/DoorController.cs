using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Animator doorAnimator;
    public AudioSource doorAudio;
    public bool hasKey = false;
    //public bool hasWalkieTalkie = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasKey)
        {
            // Schakel IsOpen naar TRUE
            doorAnimator.SetBool("IsOpen", true);

            if (doorAudio != null && !doorAudio.isPlaying)
                doorAudio.Play();

            Debug.Log("Deur schakelaar: AAN.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && hasKey)
        {
            // Schakel IsOpen naar FALSE
            doorAnimator.SetBool("IsOpen", false);

            Debug.Log("Deur schakelaar: UIT.");
        }
    }
}