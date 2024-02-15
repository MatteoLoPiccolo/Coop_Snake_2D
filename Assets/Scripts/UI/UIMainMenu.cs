using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : UIManager
{
    [Header("Main Menu")]
    [SerializeField] private Button _singleGameButton;
    [SerializeField] private Button _quitGameButton;

    private void Start()
    {
        _singleGameButton.onClick.AddListener(Play);
        _quitGameButton.onClick.AddListener(QuitGame);
    }

    protected override void OnDestroy()
    {
        _singleGameButton.onClick.RemoveListener(Play);
        _quitGameButton.onClick.RemoveListener(QuitGame);
    }
}