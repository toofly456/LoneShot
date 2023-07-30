using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int healAmount; // Heilungs-Wert für den Spieler

    private Transform player; // Referenz zum Spieler
    private bool isBeingDestroyed = false; // Flag to track if the item is being destroyed intentionally
    private bool itemSpawned = false; // Flag to track if an item has been spawned


    private void Awake()
    {
        // Find the player using the tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // We only need to subscribe to the event if the item has not been spawned yet
        if (!itemSpawned)
        {
            Bullet.OnEnemyDefeated += OnEnemyDefeatedHandler;
        }
    }

    private void OnEnemyDefeatedHandler(Vector2 defeatedEnemyPosition)
    {
        if (!itemSpawned)
        {
            // Spawn an item at the position of the defeated enemy
            SpawnItem(defeatedEnemyPosition);
            itemSpawned = true; // Set the flag to indicate that an item has been spawned
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfe, ob der Collider zum Spieler gehört
        if (other.CompareTag("Player"))
        {
            // Heile den Spieler
            HealPlayer();
            // Zerstöre das Item
            DestroyItem();
        }
    }

    private void DestroyItem()
    {
        // Set the flag to indicate that the item is being destroyed intentionally
        isBeingDestroyed = true;
        // Zerstöre das Item
        Destroy(gameObject);
    }

    private void HealPlayer()
    {
        // Suche das HealthController-Skript des Spielers und füge die Heilung hinzu
        HealthController playerHealth = player.GetComponent<HealthController>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(-healAmount); // Heilung wird als negativer Schaden betrachtet
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnEnemyDefeated event
        if (!itemSpawned)
        {
            Bullet.OnEnemyDefeated -= OnEnemyDefeatedHandler;
        }

        // If the item is being destroyed intentionally, no need to spawn a new item
        if (!isBeingDestroyed)
        {
            // Inform the GameManager that an enemy is defeated, and let it handle the item spawning
            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnEnemyDefeated(transform.position);
            }
        }
        // Reset the itemSpawned flag when the item is destroyed
        itemSpawned = false;
    }

    private void SpawnItem(Vector2 position)
    {
        // Erzeuge ein neues Item an der angegebenen Position
        GameObject newItem = Instantiate(GameManager.Instance.itemPrefab, position, Quaternion.identity);

        // Add the new item to the list of spawned items
        GameManager.Instance.spawnedItems.Add(newItem);

        // Clear all other items except the newly spawned one
        GameManager.Instance.ClearAllItemsExceptOne(newItem);
    }

}
