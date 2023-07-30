using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private float _damageAmount;  // Schaden, der dem Spieler zugef�gt werden soll

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())  // �berpr�fe, ob der Kollisionspartner die PlayerMovement-Komponente hat
        {
            var healthController = collision.gameObject.GetComponent<HealthController>();  // Holen Sie sich den HealthController des Spielers

            healthController.TakeDamage(_damageAmount);  // F�ge dem Spieler den angegebenen Schaden zu
        }
    }
}
