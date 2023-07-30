using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamagedInvincibility : MonoBehaviour
{
    [SerializeField]
    private float _invincibilityDuration;  // Dauer der Unverwundbarkeit nach Schaden

    private InvincibilityController _invincibilityController;  // Referenz auf den InvincibilityController

    private void Awake()
    {
        _invincibilityController = GetComponent<InvincibilityController>();  // Initialisiere die Referenz auf den InvincibilityController
    }

    public void StartInvincibility()
    {
        _invincibilityController.StartInvincibility(_invincibilityDuration);  // Starte die Unverwundbarkeit mit der angegebenen Dauer über den InvincibilityController
    }
}
