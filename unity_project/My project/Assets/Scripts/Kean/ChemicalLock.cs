using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using System.Runtime.CompilerServices; // Nodig voor Lists

public class ChemicalLock : MonoBehaviour
{
    [Header("De Oplossing")]
    public List<ChemicalItem> requiredIngredients; // Sleep hier de namen in (bijv: Vinegar, Salt, Lemon)
    
    [Header("Visuals & Audio")]
    public Renderer handleRenderer;       // Sleep hier de Mesh Renderer van de klink in
    public Material cleanMaterial;       // Het schone materiaal
    public AudioSource audioSource;      // AudioSource op de deur
    public AudioClip pourSound;          // Het 'glug glug' geluid
    public AudioClip powderSound;        // Optioneel: geluid voor zout (of leeg laten)
    private List<ChemicalItem> addedIngredients = new List<ChemicalItem>();
    

    public void AddChemical(ChemicalItem item)
    {
        if (item != null)
        {
            addedIngredients.Add(item);
            Destroy(item.gameObject);
            Debug.Log("Ingrediënt toegevoegd: " + item.substanceName);

            // Speel geluid
            if (item.isLiquid)
            {
                audioSource.PlayOneShot(pourSound);
            }
            else
            {
                if (powderSound != null)
                {
                    audioSource.PlayOneShot(powderSound);
                }
            }

            // Check of we alle ingrediënten hebben
            if (CheckSolution(item))
            {
                UnlockLock();
                handleRenderer.material = cleanMaterial;
            }
            else
            {
                Debug.Log("Aantal ingrediënten: " + addedIngredients.Count);
                string ingredientenString = "";
                
                foreach (ChemicalItem ci in addedIngredients)
                {
                    ingredientenString += ci.substanceName + ", ";
                }
                Debug.Log("Huidige ingrediënten: " + ingredientenString);
            }
        }      
    }
    bool CheckSolution(ChemicalItem item)
    {
        if (addedIngredients.Count != requiredIngredients.Count) 
        {
            Debug.Log("Aantal ingrediënten klopt NIET!");
            return false;
        }
        else
        {
            Debug.Log("Aantal ingrediënten klopt!");
            int matchCount = 0;
            foreach (ChemicalItem req in requiredIngredients)
            {
                foreach (ChemicalItem added in addedIngredients)
                {
                    if (req.substanceName == added.substanceName)
                    {
                        matchCount++;
                        break;
                    }
                }
            }
            if (matchCount == requiredIngredients.Count)
            {
                Debug.Log("Oplossing klopt!");
                return true;
            }
            else
            {
                Debug.Log("Oplossing klopt NIET!");
                addedIngredients.Clear();
                return false;
            }
        }



    }
    void UnlockLock()
    {
        Debug.Log("SLOT GEOPEND!");
    }

}