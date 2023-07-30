using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;  // Schaden, der dem Spieler zugefügt werden soll

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())  // Überprüfe, ob der Kollisionspartner die PlayerMovement-Komponente hat
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();  // Holen Sie sich den HealthController des Spielers

            healthController.TakeDamage(_damageAmount);  // Füge dem Spieler den angegebenen Schaden zu
        }
    }
}
