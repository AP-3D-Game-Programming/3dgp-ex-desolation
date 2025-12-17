using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Nodig voor TextMeshPro

public class KeyPadScript : MonoBehaviour
{
    [Header("Koppeling naar de Deur")]
    public SimpleKeypadDoor deDeur; // De referentie naar je nieuwe deur script
    public static bool CodeJuist { get; private set; } = false;

    [Header("Keypad Instellingen")]
    public int[] Code;
    public string CodeLength;
    public string Correct; // De juiste code (bijv. "1234")

    [Header("UI Referenties")]
    public GameObject Screen; // Het object met de TextMeshPro component

    private int Presses;
    private string result;
    private string ScreenText;
    private int reset;

    void Start()
    {
        // Maakt de array aan op basis van de opgegeven lengte
        Code = new int[(Convert.ToInt32(CodeLength))];
        Presses = 0;
    }

    void Update()
    {
        // Update de tekst op het schermpje van de keypad
        ScreenText = string.Join("", Code.Select(i => i.ToString()).ToArray());
        
        if (Screen != null)
        {
            var tmp = Screen.GetComponent<TextMeshPro>();
            if (tmp != null) tmp.text = ScreenText;
        }

        // Input detectie via de muis
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10))
            {
                // Check of we een knop raken (object met het 'Number' script)
                Number hitNumber = hit.transform.gameObject.GetComponent<Number>();

                if (hitNumber != null && Presses < Convert.ToInt32(CodeLength))
                {
                    Debug.Log("Gedrukt op: " + hit.transform.gameObject.name);
                    Code[Presses] = hitNumber.number;
                    Presses += 1;
                }

                // Check of de volledige code is ingevoerd
                if (Presses == Convert.ToInt32(CodeLength))
                {
                    result = String.Join("", new List<int>(Code).ConvertAll(i => i.ToString()).ToArray());
                    Debug.Log("Ingevoerde code: " + result);

                    if (Correct == result)
                    {
                        Debug.Log("De Code is Correct!");
                        CodeJuist = true;
                        // OPEN DE DEUR
                        if (deDeur != null)
                        {
                            deDeur.OpenDeur();
                        }
                    }
                    else
                    {
                        Debug.Log("Foute Code! Resetten...");
                        ResetKeypad();
                    }
                }
            }
        }
    }

    void ResetKeypad()
    {
        Presses = 0;
        reset = Convert.ToInt32(CodeLength) - 1;
        do
        {
            Code[reset] = 0;
            reset -= 1;
        } while (reset > -1);
    }
}