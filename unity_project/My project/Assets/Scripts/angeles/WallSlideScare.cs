using UnityEngine;
using System.Collections;

public class WallSlideScare : MonoBehaviour
{
    // --- DROPDOWN TO CHOOSE MODE ---
    public enum ScriptMode { I_Am_A_Scare, I_Am_A_Disabler }
    [Header("WHAT IS THIS OBJECT?")]
    public ScriptMode currentMode = ScriptMode.I_Am_A_Scare;

    // --- VARIABLES FOR SCARE MODE ---
    [Header("SETTINGS (If 'I Am A Scare')")]
    [Tooltip("Must be unique! e.g. 'LeftScare' or 'RightScare'")]
    public string scareID = "Scare1";
    public GameObject scaryObject; // The ghost mesh/sprite
    public Transform targetDestination; 
    public float slideSpeed = 5.0f;
    public AudioSource soundEffect;

    // --- VARIABLES FOR DISABLER MODE ---
    [Header("SETTINGS (If 'I Am A Disabler')")]
    [Tooltip("Drag the OTHER Scare script here that you want to cancel.")]
    public WallSlideScare scareToStop;

    private void Start()
    {
        // Only the Scare needs to check memory on start
        if (currentMode == ScriptMode.I_Am_A_Scare)
        {
            // Check if this scare is already done/cancelled in memory
            if (PlayerPrefs.GetInt(scareID) == 1)
            {
                DisableThisScareImmediate();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentMode == ScriptMode.I_Am_A_Scare)
            {
                // MODE A: DO THE SCARE
                if (PlayerPrefs.GetInt(scareID) == 0) // Only if not done yet
                {
                    StartCoroutine(SlideObject());
                    MarkScareAsDone(); // Save memory so it doesn't happen again
                }
            }
            else if (currentMode == ScriptMode.I_Am_A_Disabler)
            {
                // MODE B: DISABLE THE TARGET
                if (scareToStop != null)
                {
                    Debug.Log("Disabler hit! Stopping the other scare.");
                    scareToStop.MarkScareAsDone();         // Save to memory
                    scareToStop.DisableThisScareImmediate(); // Hide it now
                }
            }
        }
    }

    // --- LOGIC FUNCTIONS ---

    // Moves the object (Only used by Scare Mode)
    private IEnumerator SlideObject()
    {
        if (soundEffect != null) soundEffect.Play();

        // While not at target...
        while (Vector3.Distance(scaryObject.transform.position, targetDestination.position) > 0.01f)
        {
            scaryObject.transform.position = Vector3.MoveTowards(
                scaryObject.transform.position, targetDestination.position, slideSpeed * Time.deltaTime);
            yield return null; 
        }
        scaryObject.transform.position = targetDestination.position;
    }

    // Saves "1" to the computer's memory
    public void MarkScareAsDone()
    {
        PlayerPrefs.SetInt(scareID, 1);
        PlayerPrefs.Save();
    }

    // Hides the ghost and turns off the trigger
    public void DisableThisScareImmediate()
    {
        if (scaryObject != null) scaryObject.SetActive(false); // Hide the ghost
        this.gameObject.SetActive(false); // Disable this trigger so it can't run again
    }

    // Debug Button
    [ContextMenu("Reset Memory")]
    public void ResetMemory()
    {
        PlayerPrefs.DeleteKey(scareID);
        Debug.Log("Reset memory for " + scareID);
    }
}