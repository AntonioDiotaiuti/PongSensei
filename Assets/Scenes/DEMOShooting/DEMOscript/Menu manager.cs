using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void OnClickPlay()
    {
        SceneManager.LoadScene("Game"); 
    }
    public void OnClickQuit()
    {
        Application.Quit(); 
    }
}
