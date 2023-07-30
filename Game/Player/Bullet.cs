using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Camera _camera;  // Referenz auf die Hauptkamera

    public static event Action<Vector2> OnEnemyDefeated;  // Ereignis für die Niederlage eines Gegners

    private void Awake()
    {
        _camera = Camera.main;  // Setze die Referenz auf die Hauptkamera
    }

    private void Update()
    {
        DestroyWhenOffScreen();  // Überprüfe, ob die Kugel außerhalb des Bildschirms ist und zerstöre sie gegebenenfalls
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boss"))  // Check if the collision is with a game object tagged "Boss"
        {
            // Call a different method when the "Boss" is hit
            HandleBossHit(collision.gameObject);
        }
        else if (collision.GetComponent<EnemyMovement>())  // Check if the collision is with an object with the EnemyMovement component
        {
            Destroy(collision.gameObject);  // Destroy the colliding object (enemy)
            Destroy(gameObject);  // Destroy the bullet
        }
    }

    private void HandleBossHit(GameObject bossObject)
    {
        // Implement your custom logic here for when the "Boss" is hit
        // For example, you can destroy the boss object and trigger the event
        Destroy(bossObject);  // Destroy the boss
        OnEnemyDefeated?.Invoke(bossObject.transform.position);  // Trigger the event for defeating an enemy and pass the boss position
    }

    private void DestroyWhenOffScreen()
    {
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);  // Konvertiere die Position der Kugel in Bildschirmkoordinaten

        if (screenPosition.x < 0 ||
            screenPosition.x > _camera.pixelWidth ||
            screenPosition.y < 0 ||
            screenPosition.y > _camera.pixelHeight)
        {
            Destroy(gameObject);  // Zerstöre die Kugel, wenn sie außerhalb des Bildschirms ist
        }
    }
}

