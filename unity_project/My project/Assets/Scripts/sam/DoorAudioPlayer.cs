using UnityEngine;

public class DoorAudioPlayer : MonoBehaviour
{
    private AudioSource doorAudio;

    void Start()
    {
        // We halen de AudioSource op die op dit object staat
        doorAudio = GetComponent<AudioSource>();
    }

    // Deze functie wordt aangeroepen als er iets in de trigger stapt
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!doorAudio.isPlaying)
            {
                doorAudio.Play();
                Debug.Log("De deur gaat open... wees blij dat ik het geluid voor je afspeel.");
            }
        }
    }
}
