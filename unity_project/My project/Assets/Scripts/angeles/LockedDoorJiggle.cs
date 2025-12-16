using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [Header("Settings")]
    public AudioSource soundSource;
    public AudioClip lockedSound;
    public float shakeAmount = 0.05f;
    public float shakeDuration = 0.2f;

    [Header("Interaction")]
    public float interactDistance = 3.0f; // How close you must be to shake it
    public Transform player; // Drag your Player object here in the Inspector

    private Vector3 originalPos;
    private bool isShaking = false;

    void Start()
    {
        originalPos = transform.localPosition;

        // If you forgot to assign the player in the Inspector, try to find them automatically
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        // 1. Check if E is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // 2. Check if player exists
            if (player != null)
            {
                // 3. Check distance: Is the player close enough?
                float dist = Vector3.Distance(transform.position, player.position);
                
                if (dist <= interactDistance)
                {
                    TryOpen();
                }
            }
        }
    }

    public void TryOpen()
    {
        if (!isShaking)
        {
            if(soundSource && lockedSound) soundSource.PlayOneShot(lockedSound);
            StartCoroutine(ShakeDoor());
        }
    }

    System.Collections.IEnumerator ShakeDoor()
    {
        isShaking = true;
        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeAmount;
            transform.localPosition = new Vector3(originalPos.x + x, originalPos.y, originalPos.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = originalPos;
        isShaking = false;
    }
}