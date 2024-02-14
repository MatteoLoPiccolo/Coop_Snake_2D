using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    protected virtual void OnDestroy()
    {
        
    }

    protected void Play()
    {
        SceneManager.LoadScene(1);
    }

    protected void QuitGame()
    {
        Debug.Log("Quit from application");
        Application.Quit();
    }

    protected void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}