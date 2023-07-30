using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAwarenessController : MonoBehaviour
{
    public bool AwareOfPlayer { get; private set; }  // Gibt an, ob der Gegner den Spieler erkannt hat

    public Vector2 DirectionToPlayer { get; private set; }  // Richtung zum Spieler

    [SerializeField]
    private float _playerAwarenessDistance;  // Der Abstand, in dem der Gegner den Spieler wahrnehmen kann

    private Transform _player;  // Referenz auf den Transform des Spielers

    private void Awake()
    {
        _player = FindObjectOfType<PlayerMovement>().transform;  // Finde den Spieler in der Szene
    }

    void Update()
    {
        // Berechne den Vektor vom Gegner zum Spieler
        Vector2 enemyToPlayerVector = _player.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;  // Normalisiere den Vektor, um die Richtung zum Spieler zu erhalten

        // Überprüfe, ob der Spieler sich innerhalb des Wahrnehmungsradius des Gegners befindet
        if (enemyToPlayerVector.magnitude <= _playerAwarenessDistance)
        {
            AwareOfPlayer = true;  // Der Gegner hat den Spieler erkannt
        }
        else
        {
            AwareOfPlayer = false;  // Der Gegner hat den Spieler nicht erkannt
        }
    }
}
