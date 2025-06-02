using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlSchemeManager : MonoBehaviour
{
    public GameObject controlSchemeUI;
    public bool MenuActive;
    public delegate void EventMenu(bool Menu);
    public EventMenu OnSetMenu;
    
    

    private void Start()
    {
        if (GameSession.Instance == null || !GameSession.Instance.skipControlScheme)
        {
            controlSchemeUI.SetActive(true);
            Time.timeScale = 0f;
            MenuActive = true;
        }
        else
        {
            MenuActive = false; 
            controlSchemeUI.SetActive(false);
        }
    }
    private void Awake() 
    {
        var Menu = GameObject.FindAnyObjectByType<ControlSchemeManager>();
        if (Menu != null)
        {
           Menu.OnSetMenu += GetMenu; 
        }
    }
    public void GetMenu(bool Menu) 
    {Menu = MenuActive; 

    }

    private void Update()
    {
        if (controlSchemeUI.activeSelf && AnyInputPressed())
        {
            controlSchemeUI.SetActive(false);
            Time.timeScale = 1f;

            if (GameSession.Instance != null)
                GameSession.Instance.skipControlScheme = true;
        }
    }

    private bool AnyInputPressed()
    {
        return Input.anyKeyDown || Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0; 
    }
}
