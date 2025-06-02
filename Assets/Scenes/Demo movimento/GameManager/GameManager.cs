using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Victory UI")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;
    public GameObject firstSelectedButton; // ← assegna qui il bottone da selezionare

    [Header("Audio")]
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
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        string colorHex = "#FFFFFF";
        if (winner == "Red") colorHex = "#FF0000";
        else if (winner == "Blue") colorHex = "#0000FF";

        victoryText.text = $"<color={colorHex}>{winner}</color>";

        if (victorySound != null && audioSource != null)
        {
            audioSource.PlayOneShot(victorySound);
        }

        // Seleziona il bottone per controller/keyboard
        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null); // reset selezione
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
