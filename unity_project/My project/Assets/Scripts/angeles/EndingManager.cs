using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class EndingManager : MonoBehaviour
{
    [Header("UI Setup")]
    public CanvasGroup blackScreenGroup; // The background Fader
    public Image endingImageDisplay;     // The Picture Frame
    public GameObject creditsObject;     // The Text

    [Header("Ending Assets")]
    public Sprite labSprite;         
    public Sprite wastelandSprite;   
    public AudioClip scaryMusic;     
    public AudioClip villainThanksClip; 
    public AudioClip burnSoundClip; // <--- NEW SLOT FOR BURNING SOUND

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private bool isEnding = false; 

    void Start()
    {
        // Force UI to be invisible when game starts
        if(endingImageDisplay != null) endingImageDisplay.gameObject.SetActive(false);
        if(creditsObject != null) creditsObject.SetActive(false);
        if(blackScreenGroup != null) blackScreenGroup.alpha = 0f;
    }

    void Update()
    {
        // Allow ESC to quit to menu
        if (isEnding && Input.GetKeyDown(KeyCode.Escape))
        {
            // IMPORTANT: Unlock mouse so you can click menu buttons
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0); 
        }
    }

    public void TriggerBurnEnding()
    {
        if(!isEnding) StartCoroutine(BurnSequence());
    }

    public void TriggerReleaseEnding()
    {
        if(!isEnding) StartCoroutine(ReleaseSequence());
    }

    // --- SEQUENCE 1: BURN (Button) ---
    IEnumerator BurnSequence()
    {
        isEnding = true;
        DisablePlayerControls();

        // 1. INSTANT BLACK CUT
        if(blackScreenGroup != null) blackScreenGroup.alpha = 1f;

        // 2. Play Burn Sound in the dark
        if(burnSoundClip != null) sfxSource.PlayOneShot(burnSoundClip);

        // 3. Wait 2 seconds for the sound to sizzle
        yield return new WaitForSeconds(2f);

        // 4. Reveal Lab, Credits, and Scary Music
        endingImageDisplay.sprite = labSprite;
        endingImageDisplay.gameObject.SetActive(true);
        creditsObject.SetActive(true);

        musicSource.clip = scaryMusic;
        musicSource.Play();
    }

    // --- SEQUENCE 2: RELEASE (Lever) ---
    IEnumerator ReleaseSequence()
    {
        isEnding = true;
        DisablePlayerControls();

        // 1. INSTANT BLACK CUT
        if(blackScreenGroup != null) blackScreenGroup.alpha = 1f;

        // 2. Play Villain Voice in the dark
        if(villainThanksClip != null) sfxSource.PlayOneShot(villainThanksClip);

        // 3. Wait for voice to finish
        if(villainThanksClip != null) 
            yield return new WaitForSeconds(villainThanksClip.length);
        else 
            yield return new WaitForSeconds(2f);

        // 4. Reveal Wasteland, Credits, and Scary Music
        endingImageDisplay.sprite = wastelandSprite;
        endingImageDisplay.gameObject.SetActive(true);
        // FORCE VISIBILITY (Fix for invisible image bug)
        endingImageDisplay.color = Color.white; 

        creditsObject.SetActive(true);
        
        musicSource.clip = scaryMusic;
        musicSource.Play();

        // 5. Wait 10 seconds (let credits roll)
        //yield return new WaitForSeconds(10f); 

        // 6. Cut back to pure black
        //endingImageDisplay.gameObject.SetActive(false); 
        //creditsObject.SetActive(false);
    }

    void DisablePlayerControls()
    {
        // Finds player and stops movement
        var player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            // MAKE SURE THIS NAME MATCHES YOUR SCRIPT EXACTLY
            var movement = player.GetComponent<First_Person_Movement>();
            if(movement != null) movement.enabled = false;
        }
    }
}