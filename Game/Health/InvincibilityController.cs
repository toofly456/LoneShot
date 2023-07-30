using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvincibilityController : MonoBehaviour
{
    private HealthController _healthController;  // Referenz auf den HealthController des Spielers

    private void Awake()
    {
        _healthController = GetComponent<HealthController>();  // Initialisiere die Referenz auf den HealthController
    }

    public void StartInvincibility(float invincibilityDuration)
    {
        StartCoroutine(InvincibilityCoroutine(invincibilityDuration));  // Starte die Unverwundbarkeitskoroutine mit der angegebenen Dauer
    }

    private IEnumerator InvincibilityCoroutine(float invincibilityDuration)
    {
        _healthController.IsInvincible = true;  // Setze den Unverwundbarkeitsstatus des Spielers auf "true"
        yield return new WaitForSeconds(invincibilityDuration);  // Warte für die angegebene Dauer
        _healthController.IsInvincible = false;  // Setze den Unverwundbarkeitsstatus des Spielers auf "false"
    }
}
