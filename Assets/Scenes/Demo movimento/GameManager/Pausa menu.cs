using UnityEngine;
using UnityEngine.EventSystems;

public class Pausamenu : MonoBehaviour
{
    public static bool GameisPaused = false;

    [Header("UI")]
    public GameObject pauseMenuUI;
    public GameObject firstSelectedButton;

    private void Update()
    {
        // Supporta ESC e Start del controller
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Start"))
        {
            if (GameisPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameisPaused = false;

        // Rimuove qualsiasi selezione UI residua
        EventSystem.current.SetSelectedGameObject(null);
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameisPaused = true;

        // Seleziona il primo bottone per il controller
        EventSystem.current.SetSelectedGameObject(null); // reset selezione
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

