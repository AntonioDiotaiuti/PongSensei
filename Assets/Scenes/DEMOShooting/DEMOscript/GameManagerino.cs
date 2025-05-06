using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerino : MonoBehaviour
{
    public static GameManagerino Instance;
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    private void Awake()
    {
        // Verifica che l'istanza non sia già assegnata
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);  // Assicura che ci sia solo un GameManagerino
        }
    }

    public void DeclareVictory(string winner)
    {
        Time.timeScale = 0f; // Congela il tempo
        victoryPanel.SetActive(true); // Mostra il pannello di vittoria
        victoryText.text = winner + " ha vinto!"; // Mostra il testo con il vincitore
    }

    // Questo viene chiamato dal bottone Restart
    public void RestartGame()
    {
        Time.timeScale = 1f; // Ripristina il flusso del tempo prima di ricaricare la scena
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name); // Ricarica la scena corrente
    }
}
