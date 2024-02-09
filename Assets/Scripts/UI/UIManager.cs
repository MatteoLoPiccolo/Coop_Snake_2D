using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(gameObject);
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
            _instance = null;
    }

    protected void StartGame()
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