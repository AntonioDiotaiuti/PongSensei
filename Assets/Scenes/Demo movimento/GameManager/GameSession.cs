using UnityEngine;

public class GameSession : MonoBehaviour
{
    public static GameSession Instance;

    public bool skipControlScheme = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ResetSession()
    {
        skipControlScheme = false;
    }
}
