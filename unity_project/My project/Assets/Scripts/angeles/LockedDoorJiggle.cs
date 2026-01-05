using UnityEngine;
using System.Collections;

public class LockedDoorFinal : MonoBehaviour
{
    [Header("Assign These")]
    public AudioSource audioSource;
    public AudioClip lockedSound;
    [Tooltip("Drag your '[E] Locked' text object here")]
    public GameObject promptUI; // <--- Drag your text here

    [Header("Settings")]
    public float shakeTime = 0.2f;
    public float shakeStrength = 0.05f;

    private bool playerIsClose = false;
    private bool isShaking = false;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
        
        // Ensure text is hidden when game starts
        if (promptUI != null) promptUI.SetActive(false);
    }

    // When you walk INTO the invisible box
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            if (promptUI != null) promptUI.SetActive(true); // Show Text
        }
    }

    // When you walk OUT OF the invisible box
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            if (promptUI != null) promptUI.SetActive(false); // Hide Text
        }
    }

    void Update()
    {
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            if (!isShaking) StartCoroutine(ShakeRoutine());
        }
    }

    IEnumerator ShakeRoutine()
    {
        isShaking = true;
        
        if (audioSource && lockedSound) audioSource.PlayOneShot(lockedSound);

        float elapsed = 0f;
        while (elapsed < shakeTime)
        {
            float x = Random.Range(-1f, 1f) * shakeStrength;
            float y = Random.Range(-1f, 1f) * shakeStrength;

            transform.localPosition = startPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = startPos;
        isShaking = false;
    }
}