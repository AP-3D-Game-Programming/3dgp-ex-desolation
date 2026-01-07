using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class KeyPadScriptModified : MonoBehaviour
{
    [Header("1. UI & Objects")]
    public GameObject Screen;   
    public GameObject crosshair; 
    
    [Header("Interaction Text")]
    [Tooltip("Drag your Text (TMP) object here.")]
    public TMP_Text promptText; 

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
            // 1. IS IT A KEYPAD BUTTON? (Requires Left Click)
            Number hitNumber = hit.transform.GetComponent<Number>();
            if (hitNumber != null)
            {
                foundSomething = true;
                ShowPrompt("[Click] " + hitNumber.number); 

                // INPUT CHECK: LEFT CLICK
                if (Input.GetMouseButtonDown(0))
                {
                    HandleInteraction(hit);
                }
            }
            
            // 2. IS IT THE EXIT DOOR? (Requires E)
            else if (hit.transform.CompareTag("ExitDoor"))
            {
                foundSomething = true;
                
                if (isCodeCorrect)
                {
                    ShowPrompt("[E] Open");
                    
                    // INPUT CHECK: E KEY
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        HandleInteraction(hit);
                    }
                }
                else
                {
                    ShowPrompt("Locked");
                }
            }
        }

        // --- VISUALS LOGIC ---
        // Hide text and crosshair if looking at nothing
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

        // (Note: The generic Input block was removed from here and moved 
        // specifically into the button/door checks above).
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