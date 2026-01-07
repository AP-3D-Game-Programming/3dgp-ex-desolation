using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Needed for Text Mesh Pro
using UnityEngine.SceneManagement;

public class KeyPadScriptModified : MonoBehaviour
{
    [Header("1. UI & Objects")]
    public GameObject Screen;   // The screen on the keypad
    public GameObject crosshair; // The dot in the center
    
    [Header("Interaction Text")]
    [Tooltip("Drag your Text (TMP) object here. Can be 3D text or UI text.")]
    public TMP_Text promptText; // <--- NEW: Drag your text object here

    [Header("2. Settings")]
    public string nextSceneName = "Level2"; 
    public string CorrectCode = "1234"; 
    public int CodeLength = 4;

    private int[] currentInput;
    private int pressesCount = 0;
    private bool isCodeCorrect = false;

    void Start()
    {
        currentInput = new int[CodeLength];
        
        // Hide things at the start so they don't block view
        if (crosshair != null) crosshair.SetActive(false);
        if (promptText != null) promptText.gameObject.SetActive(false);
    }

    void Update()
    {
        // --- KEYPAD SCREEN LOGIC ---
        if (Screen != null)
        {
            string textToShow = "";
            for(int i=0; i < pressesCount; i++) textToShow += currentInput[i].ToString();
            
            var tmp = Screen.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = isCodeCorrect ? "OPEN" : textToShow;
        }

        // --- RAYCAST LOGIC (Eyes) ---
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        
        bool foundSomething = false;

        // Shoot a ray 5 meters forward
        if (Physics.Raycast(ray, out hit, 5))
        {
            // 1. IS IT A KEYPAD BUTTON?
            Number hitNumber = hit.transform.GetComponent<Number>();
            if (hitNumber != null)
            {
                foundSomething = true;
                ShowPrompt("[Click] " + hitNumber.number); // Changes text to "CLICK 1" etc
            }
            
            // 2. IS IT THE EXIT DOOR?
            else if (hit.transform.CompareTag("ExitDoor"))
            {
                foundSomething = true;
                
                if (isCodeCorrect)
                {
                    ShowPrompt("[E] Open");
                }
                else
                {
                    ShowPrompt("Locked");
                }
            }
        }

        // Hide text if looking at nothing
        if (!foundSomething)
        {
            if (promptText != null) promptText.gameObject.SetActive(false);
            if (crosshair != null) crosshair.SetActive(false);
        }
        else
        {
            // Show crosshair if we found something
            if (crosshair != null) crosshair.SetActive(true);
        }

        // --- INPUT LOGIC ---
        // Only works if we are actually looking at something valid
        if (foundSomething && Input.GetMouseButtonDown(0))
        {
            HandleInteraction(hit);
        }
    }

    void ShowPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
            promptText.gameObject.SetActive(true);
        }
    }

    void HandleInteraction(RaycastHit hit)
    {
        // Handle Button Press
        Number hitNumber = hit.transform.GetComponent<Number>();
        if (hitNumber != null)
        {
            if (pressesCount < CodeLength && !isCodeCorrect)
            {
                currentInput[pressesCount] = hitNumber.number;
                pressesCount++;
                CheckCode();
            }
            return; 
        }

        // Handle Door Open
        if (hit.transform.CompareTag("ExitDoor") && isCodeCorrect)
        {
            Debug.Log("Loading Next Level...");
            SceneManager.LoadScene(nextSceneName);
        }
    }

    void CheckCode()
    {
        if (pressesCount == CodeLength)
        {
            string result = "";
            for(int i=0; i < pressesCount; i++) result += currentInput[i].ToString();

            if (result == CorrectCode) isCodeCorrect = true;
            else { 
                // Wrong code: Reset
                pressesCount = 0; 
                Array.Clear(currentInput, 0, CodeLength); 
            }
        }
    }
}