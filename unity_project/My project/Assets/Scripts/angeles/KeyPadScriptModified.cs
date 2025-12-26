using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement;

public class KeyPadScriptModified : MonoBehaviour
{
    [Header("1. SETUP")]
    public GameObject Screen; // DRAG YOUR TEXT OBJECT HERE AGAIN!
    public string nextSceneName = "3_Biolab"; 
    
    [Header("2. CODE SETTINGS")]
    public string CorrectCode = "1984"; 
    public int CodeLength = 4;

    // Internal variables
    private int[] currentInput;
    private int pressesCount = 0;
    private bool isCodeCorrect = false;

    void Start()
    {
        currentInput = new int[CodeLength];
    }

    void Update()
    {
        // --- UPDATE SCREEN TEXT ---
        if (Screen != null)
        {
            string textToShow = "";
            for(int i=0; i < pressesCount; i++) {
                textToShow += currentInput[i].ToString();
            }
            
            var tmp = Screen.GetComponent<TextMeshPro>();
            if (tmp != null) 
            {
                if(isCodeCorrect) tmp.text = "OPEN";
                else tmp.text = textToShow;
            }
        }

        // --- INTERACTION ---
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E))
        {
            HandleInteraction();
        }
    }

    void HandleInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5))
        {
            Debug.Log("I HIT: " + hit.transform.name); // <--- READ THIS IN CONSOLE

            // 1. CHECK IF WE HIT A BUTTON
            Number hitNumber = hit.transform.GetComponent<Number>();
            if (hitNumber != null)
            {
                if (pressesCount < CodeLength && !isCodeCorrect)
                {
                    Debug.Log("Button Pressed: " + hitNumber.number);
                    currentInput[pressesCount] = hitNumber.number;
                    pressesCount++;
                    CheckCode();
                }
                return; // Stop here if we hit a button
            }

            // 2. CHECK IF WE HIT THE DOOR (Look for Tag "ExitDoor")
            if (hit.transform.CompareTag("ExitDoor"))
            {
                if (isCodeCorrect)
                {
                    Debug.Log("DOOR UNLOCKED! LOADING SCENE...");
                    SceneManager.LoadScene(nextSceneName);
                }
                else
                {
                    Debug.Log("DOOR IS LOCKED. FINISH THE CODE FIRST.");
                    // If you see this message when clicking a button, 
                    // YOUR DOOR COLLIDER IS BLOCKING THE BUTTONS!
                }
            }
        }
    }

    void CheckCode()
    {
        if (pressesCount == CodeLength)
        {
            string result = "";
            for(int i=0; i < pressesCount; i++) result += currentInput[i].ToString();

            if (result == CorrectCode)
            {
                Debug.Log("CODE CORRECT!");
                isCodeCorrect = true;
            }
            else
            {
                Debug.Log("WRONG CODE. RESETTING.");
                pressesCount = 0;
                Array.Clear(currentInput, 0, CodeLength);
            }
        }
    }
}