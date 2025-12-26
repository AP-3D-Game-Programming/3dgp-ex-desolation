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
    }

    void Update()
    {
        // Screen Logic
        if (Screen != null)
        {
            string textToShow = "";
            for(int i=0; i < pressesCount; i++) textToShow += currentInput[i].ToString();
            var tmp = Screen.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = isCodeCorrect ? "OPEN" : textToShow;
        }

        // --- HOVER LOGIC ---
        // 1. Create a "LayerMask" to ignore the Player (Layer 2 is typically Ignore Raycast)
        // Note: For now we just shoot normal rays.
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        bool isHovering = false;

        if (Physics.Raycast(ray, out hit, 5))
        {
            // Check for Button
            if (hit.transform.GetComponent<Number>() != null)
            {
                isHovering = true;
                // DEBUG: Uncomment the line below if you want to see what button triggers it
                // Debug.Log("Crosshair ON because of Number: " + hit.transform.name);
            }
            // Check for Door
            else if (hit.transform.CompareTag("ExitDoor"))
            {
                isHovering = true;
                // THIS IS THE SNITCH LINE:
                Debug.Log("Crosshair ON because I see 'ExitDoor' tag on: " + hit.transform.name);
            }
        }

        // Set Crosshair
        if (crosshair != null) crosshair.SetActive(isHovering);

        // Click Logic
        if (isHovering && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)))
        {
            HandleInteraction(hit);
        }
    }

    void HandleInteraction(RaycastHit hit)
    {
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

        if (hit.transform.CompareTag("ExitDoor") && isCodeCorrect)
        {
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
            else { pressesCount = 0; Array.Clear(currentInput, 0, CodeLength); }
        }
    }
}