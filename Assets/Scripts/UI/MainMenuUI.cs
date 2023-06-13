using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;

    private void Awake()
    {
        Time.timeScale = 1f;
        EventSystem.current.SetSelectedGameObject(null);

        playButton.onClick.AddListener
        (
            () => 
            {
                Loader.Load(Loader.Scene.GameScene);
            }
        );
        quitButton.onClick.AddListener
        (
            () => 
            {
                Application.Quit();
            }
        );
    }
}


