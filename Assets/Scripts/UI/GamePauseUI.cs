using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button optionButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            MyGameManager.Instance.TogglePuaseGame();
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        optionButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });
    }
    private void Start()
    {
        MyGameManager.Instance.OnGamePaused += MyGameManager_OnGamePaused;
        MyGameManager.Instance.OnGameUnpaused += MyGameManager_OnGameUnpaused;

        Hide();
    }

    private void MyGameManager_OnGameUnpaused(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void MyGameManager_OnGamePaused(object sender, System.EventArgs e)
    {
        EventSystem.current.SetSelectedGameObject(null);
        Show();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        resumeButton.Select();
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
