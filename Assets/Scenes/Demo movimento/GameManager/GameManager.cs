using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void DeclareVictory(string winner)
    {
        Time.timeScale = 0f; // blocca il gioco
        victoryPanel.SetActive(true);
        victoryText.text = winner + " ha vinto!";
    }

    // Questo viene chiamato dal bottone
    public void RestartGame()
    {
        Time.timeScale = 1f; // sblocca il tempo prima di ricaricare
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
