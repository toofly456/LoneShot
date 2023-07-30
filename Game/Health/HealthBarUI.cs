using UnityEngine;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField]
    private UnityEngine.UI.Image _healthBarForegroundImage;  // Referenz auf das Bild der Vordergrundanzeige der Gesundheitsleiste

    public void UpdateHealthBar(HealthController healthController)
    {
        _healthBarForegroundImage.fillAmount = healthController.RemainingHealthPercentage;  // Aktualisiere den Füllwert der Vordergrundanzeige basierend auf dem prozentualen verbleibenden Gesundheitswert des HealthControllers
    }
}
