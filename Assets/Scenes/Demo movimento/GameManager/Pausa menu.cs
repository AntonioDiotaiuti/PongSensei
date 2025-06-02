using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class Pausamenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject firstSelectedButton;

    [Header("Colori testo")]
    public Color normalColor = Color.white;
    public Color selectedColor = Color.yellow;

    private List<TextMeshProUGUI> buttonTexts = new List<TextMeshProUGUI>();

    private void Start()
    {
        // Raccoglie tutti i TextMeshProUGUI dei bottoni nel menu pausa
        Button[] buttons = pauseMenuUI.GetComponentsInChildren<Button>(true);
        foreach (var btn in buttons)
        {
            TextMeshProUGUI tmp = btn.GetComponentInChildren<TextMeshProUGUI>();
            if (tmp != null)
                buttonTexts.Add(tmp);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (GameisPaused)
                Resume();
            else
                Pause();
        }

        if (GameisPaused)
            UpdateTextHighlighting();
    }

    void UpdateTextHighlighting()
    {
        GameObject current = EventSystem.current.currentSelectedGameObject;

        foreach (var text in buttonTexts)
        {
            if (text == null) continue;
            // Evidenzia se il padre è il selezionato
            bool isSelected = current != null && text.transform.IsChildOf(current.transform);
            text.color = isSelected ? selectedColor : normalColor;
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}


