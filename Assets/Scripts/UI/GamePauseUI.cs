using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePauseUI : MonoBehaviour
{
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
        Show();

    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
