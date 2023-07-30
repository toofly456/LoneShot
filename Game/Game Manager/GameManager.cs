using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI enemyCountText;  // Referenz auf das TextMeshProUGUI-Objekt für die Anzeige der Anzahl der besiegten Gegner
    [SerializeField]
    public GameObject itemPrefab;  // Prefab des Items, der gespawnt werden soll

    private int enemyCount;  // Anzahl der besiegten Gegner
    private int highScore;  // Highscore

    public List<GameObject> spawnedItems = new List<GameObject>(); // List to store references to spawned items

    // Singleton instance of the GameManager
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);  // Lade den Highscore aus den PlayerPrefs
        UpdateEnemyCountText();
    }

    private void OnDestroy()
    {
        // Singleton pattern
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void OnEnemyDefeated(Vector2 defeatedEnemyPosition)
    {
        enemyCount++;  // Erhöhe die Anzahl der besiegten Gegner

        // Update the score (high score is handled separately)
        UpdateEnemyCountText();

        // Your additional logic here, if any

        // Spawn an item at the position of the defeated enemy
        SpawnItem(defeatedEnemyPosition);
    }

    public void ClearAllItemsExceptOne(GameObject itemToKeep)
    {
        // Iterate through the list of spawned items
        for (int i = spawnedItems.Count - 1; i >= 0; i--)
        {
            // If the item is not the one to keep, destroy it and remove it from the list
            if (spawnedItems[i] != itemToKeep)
            {
                Destroy(spawnedItems[i]);
                spawnedItems.RemoveAt(i);
            }
        }
    }

    public void ClearSpawnedItems()
    {
        // Destroy all spawned items
        foreach (GameObject item in spawnedItems)
        {
            Destroy(item);
        }
        spawnedItems.Clear(); // Clear the list of spawned items
    }
    private void SpawnItem(Vector2 position)
    {
        // Erzeuge ein neues Item an der angegebenen Position
        Instantiate(itemPrefab, position, Quaternion.identity);
    }

    private void UpdateEnemyCountText()
    {
        enemyCountText.text = $"Defeated Enemies: {enemyCount}\nHighScore: {highScore}";  // Aktualisiere den Text für die Anzeige der Anzahl der besiegten Gegner und des Highscores
    }
}
