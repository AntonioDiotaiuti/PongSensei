using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Victory UI")]
    public GameObject victoryPanel;
    public TextMeshProUGUI victoryText;
    public GameObject firstSelectedButton;

    [Header("Controls Screen")]
    public GameObject controlsPanel;

    [Header("Audio")]
    public AudioClip victorySound;
    private AudioSource audioSource;

    private bool waitingForInput = false;
    private static bool hasStarted = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        if (!hasStarted)
        {
            ShowControls();
            hasStarted = true;
        }
    }

    private void ShowControls()
    {
        if (controlsPanel != null)
        {
            controlsPanel.SetActive(true);
            Time.timeScale = 0f;
            waitingForInput = true;
        }
    }

    private void Update()
    {
        if (waitingForInput && Input.anyKeyDown)
        {
            controlsPanel.SetActive(false);
            StartCoroutine(ResumeGameNextFrame());
        }
    }

    private IEnumerator ResumeGameNextFrame()
    {
        yield return null; // attende un frame
        Time.timeScale = 1f;
        waitingForInput = false;
    }

    public bool IsWaitingForInput()
    {
        return waitingForInput;
    }

    public void DeclareVictory(string winner)
    {
        Time.timeScale = 0f;
        victoryPanel.SetActive(true);

        string colorHex = "#FFFFFF";
        if (winner == "Red") colorHex = "#FF0000";
        else if (winner == "Blue") colorHex = "#0000FF";

        victoryText.text = $"<color={colorHex}>{winner}</color>";

        if (victorySound != null)
            audioSource.PlayOneShot(victorySound);

        if (firstSelectedButton != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstSelectedButton);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Non resettiamo hasStarted → lo schema non riappare
    }
}



