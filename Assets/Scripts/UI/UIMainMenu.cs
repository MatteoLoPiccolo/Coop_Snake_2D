using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIManager
{
    [Header("Main Menu")]
    [SerializeField] Button _singleGameButton;
    [SerializeField] Button _coopGameButton;
    [SerializeField] Button _quitGameButton;

    private void Start()
    {
        _singleGameButton.onClick.AddListener(StartGame);
        _coopGameButton.onClick.AddListener(StartGame);
        _quitGameButton.onClick.AddListener(QuitGame);
    }

    protected override void OnDestroy()
    {
        _singleGameButton.onClick.RemoveListener(StartGame);
        _coopGameButton.onClick.RemoveListener(StartGame);
        _quitGameButton.onClick.RemoveListener(QuitGame);
    }
}