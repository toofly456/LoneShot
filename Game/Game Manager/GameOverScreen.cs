using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Show()
    {
        gameObject.SetActive(true);  // Aktiviere das Game Over-Screen
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("SampleScene"); // Neustart
    }

    public void ExitButton()
    {
        UnityEditor.EditorApplication.isPlaying = false; // Beende die Spielansicht im Editor
        // Alternativ, wenn du die obige Zeile in einem Build verwenden möchtest:
        // Application.Quit();
    }
}
