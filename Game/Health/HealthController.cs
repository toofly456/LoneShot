using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float _currentHealth;  // Aktuelle Gesundheit des Objekts

    [SerializeField]
    private float _maximumHealth;  // Maximale Gesundheit des Objekts

    public float RemainingHealthPercentage  // Prozentsatz der verbleibenden Gesundheit
    {
        get
        {
            return _currentHealth / _maximumHealth;
        }
    }

    public bool IsInvincible { get; set; }  // Gibt an, ob das Objekt unverwundbar ist

    public UnityEvent OnDied;  // Ereignis, das ausgelöst wird, wenn das Objekt stirbt

    public UnityEvent OnDamaged;  // Ereignis, das ausgelöst wird, wenn das Objekt Schaden nimmt

    public UnityEvent OnHealthChanged;  // Ereignis, das ausgelöst wird, wenn sich die Gesundheit ändert

    public GameOverScreen GameOverScreen;
    private void GameOver()
    {
        GameOverScreen.Show();  // Zeige das Game Over-Screen an
    }

    public void TakeDamage(float damageAmount)
    {
        if (_currentHealth == 0)
        {
            return;
        }

        if (IsInvincible)
        {
            return;
        }

        _currentHealth -= damageAmount;  // Verringere die Gesundheit um den Schadenbetrag

        OnHealthChanged.Invoke();  // Löse das Ereignis für die Gesundheitsänderung aus

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        if (_currentHealth == 0)
        {
            OnDied.Invoke();  // Löse das Ereignis für den Tod aus
            GameOver();  // Zeige das Game Over-Screen an
        }
        else
        {
            OnDamaged.Invoke();  // Löse das Ereignis für Schaden aus
        }
    }
}
