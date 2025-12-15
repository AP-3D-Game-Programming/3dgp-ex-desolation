using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float respawnTime = 3f;

    private GameObject currentItem;
    private bool isRespawning = false;

    void Start()
    {
        if (itemPrefab == null)
        {
            Debug.LogError("FOUT: Je bent vergeten de Prefab in het vakje te slepen!");
            return;
        }
        SpawnItem();
    }

    void Update()
    {
        // Alleen respawnen als het item ECHT weg is (destroyed)
        if (currentItem == null && !isRespawning)
        {
            StartCoroutine(RespawnRoutine());
        }
    }

    void SpawnItem()
    {
        Debug.Log("SPAWNER: Ik probeer nu een item te maken...");
        
        // We gebruiken transform.position en rotation van de Spawner zelf
        currentItem = Instantiate(itemPrefab, transform.position, transform.rotation);
        
        // Forceer de naam zodat je hem kunt vinden in de lijst
        currentItem.name = itemPrefab.name;

        Debug.Log($"SPAWNER: Gelukt! Item staat op positie: {currentItem.transform.position}");
    }

    IEnumerator RespawnRoutine()
    {
        Debug.Log("SPAWNER: Item is weg! Wachten op respawn...");
        isRespawning = true;
        yield return new WaitForSeconds(respawnTime);
        SpawnItem();
        isRespawning = false;
    }
}