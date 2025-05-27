using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;

    public AudioClip victorySound;
    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    public void DeclareVictory(string winner)
    {
        Debug.Log("sfsdg");
        Time.timeScale = 0f; // blocca il gioco
        victoryPanel.SetActive(true);
        victoryText.text = winner + " ha vinto!";

        // Suona l'audio di vittoria
        if (victorySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(victorySound);
        }
    }

    // Questo viene chiamato dal bottone
    public void RestartGame()
    {
        Time.timeScale = 1f; // sblocca il tempo prima di ricaricare
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
