using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIManager
{
    [Header("Game Over")]
    [SerializeField] GameObject _gameOverObj;
    [SerializeField] Button _restartGameButton;
    [SerializeField] Button _mainMenuButton;

    [Space]
    [Header("Pause")]
    [SerializeField] GameObject _pauseObj;

    private static UILevel _instance;
    public static UILevel Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
            Destroy(gameObject);

        _gameOverObj.SetActive(false);
    }

    private void Start()
    {
        _restartGameButton.onClick.AddListener(Play);
        _mainMenuButton.onClick.AddListener(MainMenu);
    }

    protected override void OnDestroy()
    {
        _restartGameButton.onClick.RemoveListener(Play);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void GameOver()
    {
        _gameOverObj.SetActive(true);
    }

    public void Pause()
    {
        _pauseObj.SetActive(true);
    }

    public void Unpause()
    {
        _pauseObj.SetActive(false);
    }
}