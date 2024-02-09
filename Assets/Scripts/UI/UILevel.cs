using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIManager
{
    [Header("Game Over")]
    [SerializeField] GameObject _gameOverObj;
    [SerializeField] Button _restartGameButton;
    [SerializeField] Button _mainMenuButton;

    private static UILevel _instance;
    public new static UILevel Instance {  get { return _instance; } }

    protected override void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);

        _gameOverObj.SetActive(false);
    }

    private void Start()
    {
        _restartGameButton.onClick.AddListener(StartGame);
        _mainMenuButton.onClick.AddListener(MainMenu);
    }

    protected override void OnDestroy()
    {
        _restartGameButton.onClick.RemoveListener(StartGame);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void GameOver()
    {
        _gameOverObj.SetActive(true);
    }
}