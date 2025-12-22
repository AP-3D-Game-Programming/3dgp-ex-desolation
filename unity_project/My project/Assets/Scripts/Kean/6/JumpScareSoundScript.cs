using UnityEngine;

public class JumpScareSoundScript : MonoBehaviour
{
    public AudioSource jumpScareAudioSource;
    public AudioClip jumpScareClip;
    private bool AudioPlayed = false;

    void OnTriggerEnter()
    {
        if (!AudioPlayed)
        {
            jumpScareAudioSource.PlayOneShot(jumpScareClip);
            AudioPlayed = true;
        }
    }
}
