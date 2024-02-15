using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIManager
{
    [Header("Players reference")]
    [SerializeField] private SnakeController _snakeController1;
    [SerializeField] private SnakeController _snakeController2;

    [Header("Game Over")]
    [SerializeField] private GameObject _gameOverObj;
    [SerializeField] private Button _restartGameButton;
    [SerializeField] private Button _mainMenuButton;

    [Space]
    [Header("Pause")]
    [SerializeField] private GameObject _pauseObj;

    [Space]
    [Header("Score")]
    [SerializeField] private GameObject _scoreOne;
    [SerializeField] private GameObject _scoreTwo;
    [SerializeField] private TextMeshProUGUI _scoreOneText;
    [SerializeField] private TextMeshProUGUI _scoreTwoText;

    [Space]
    [Header("Win UI")]
    [SerializeField] private GameObject _winObj;
    [SerializeField] private TextMeshProUGUI _winText;

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
        SetScoreText();

        _scoreOne.SetActive(false);
        _scoreTwo.SetActive(false);
        _winObj.SetActive(false);

        _restartGameButton.onClick.AddListener(Play);
        _mainMenuButton.onClick.AddListener(MainMenu);
    }

    private void SetScoreText()
    {
        _scoreOneText.text = "Snake 1 score : " + _snakeController1.Score;
        _scoreTwoText.text = "Snake 2 score : " + _snakeController2.Score;
    }

    protected override void OnDestroy()
    {
        _restartGameButton.onClick.RemoveListener(Play);
        _mainMenuButton.onClick.RemoveListener(MainMenu);
    }

    public void UpdateSnakeOneScore()
    {
        _scoreOne.SetActive(true);
        _scoreOneText.text = "Snake 1 score: " + _snakeController1.Score.ToString();
        Invoke("ScoreOneDisappear", 0.7f);
    }

    public void UpdateSnakeTwoScore()
    {
        _scoreTwo.SetActive(true);
        _scoreTwoText.text = "Snake 2 score: " + _snakeController2.Score.ToString();
        Invoke("ScoreTwoDisappear", 0.7f);
    }

    private void ScoreOneDisappear()
    {
        _scoreOne.SetActive(false);
    }

    private void ScoreTwoDisappear()
    {
        _scoreTwo.SetActive(false);
    }

    public void GameOver()
    {
        _gameOverObj.SetActive(true);
    }

    public void WinScreen()
    {
        _winObj.SetActive(true);

        if(_snakeController1.IsHitHimself())
        {
            _winText.text = "Snake 1 lost! You eat yourself!";
            return;
        }

        if (_snakeController2.IsHitHimself())
        {
            _winText.text = "Snake 2 lost! You eat yourself!";
            return;
        }

        if (_snakeController1.Score > _snakeController2.Score)
            _winText.text = "Snake 1 win! Your score is " + _snakeController1.Score;
        else if (_snakeController1.Score < _snakeController2.Score)
            _winText.text = "Snake 2 win! Your score is " + _snakeController2.Score;
        else
            _winText.text = "Draw! " + "Snake1 score : " + _snakeController1.Score + " and Snake2 sore is : " + _snakeController2.Score;
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