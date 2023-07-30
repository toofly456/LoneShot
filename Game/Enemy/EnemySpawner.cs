using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;  // Prefab des Gegners, der gespawnt werden soll

    [SerializeField]
    private float _minimumSpawnTime;  // Minimale Wartezeit zwischen den Spawns

    [SerializeField]
    private float _maximumSpawnTime;  // Maximale Wartezeit zwischen den Spawns

    private float _timeUntilSpawn;  // Verbleibende Zeit bis zum n�chsten Spawn

    void Awake()
    {
        SetTimeUntilSpawn();  // Setze die anf�ngliche Zeit bis zum n�chsten Spawn
    }

    void Update()
    {
        _timeUntilSpawn -= Time.deltaTime;  // Verringere die verbleibende Zeit bis zum n�chsten Spawn basierend auf der vergangenen Zeit seit dem letzten Frame

        if (_timeUntilSpawn <= 0)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);  // Erzeuge einen neuen Gegner an der Position des Spawners mit der Standardrotation
            SetTimeUntilSpawn();  // Setze die Zeit bis zum n�chsten Spawn
        }
    }

    private void SetTimeUntilSpawn()
    {
        _timeUntilSpawn = Random.Range(_minimumSpawnTime, _maximumSpawnTime);  // Setze eine zuf�llige Zeit bis zum n�chsten Spawn basierend auf den angegebenen minimalen und maximalen Wartezeiten
    }
}
