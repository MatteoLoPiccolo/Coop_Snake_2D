using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : UIManager
{
    [Header("Players reference")]
    [SerializeField] SnakeController _snakeController1;
    [SerializeField] SnakeController _snakeController2;

    [Header("Game Over")]
    [SerializeField] GameObject _gameOverObj;
    [SerializeField] Button _restartGameButton;
    [SerializeField] Button _mainMenuButton;

    [Space]
    [Header("Pause")]
    [SerializeField] GameObject _pauseObj;

    [Space]
    [Header("Score")]
    //[SerializeField] GameObject _scoreObj;
    [SerializeField] GameObject _scoreOne;
    [SerializeField] GameObject _scoreTwo;
    [SerializeField] TextMeshProUGUI _scoreOneText;
    [SerializeField] TextMeshProUGUI _scoreTwoText;

    [Space]
    [Header("Win UI")]
    [SerializeField] GameObject _winObj;
    [SerializeField] TextMeshProUGUI _winText;

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
        StartCoroutine(ScoreOneDisappear());
    }

    public void UpdateSnakeTwoScore()
    {
        _scoreTwo.SetActive(true);
        _scoreTwoText.text = "Snake 2 score: " + _snakeController2.Score.ToString();
        StartCoroutine(ScoreTwoDisappear());
    }

    IEnumerator ScoreOneDisappear()
    {
        yield return new WaitForSeconds(0.5f);
        _scoreOne.SetActive(false);
    }

    IEnumerator ScoreTwoDisappear()
    {
        yield return new WaitForSeconds(0.5f);
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