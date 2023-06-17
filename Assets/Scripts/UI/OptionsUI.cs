using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button moveUpButton;
    [SerializeField] private Button moveDownButton;
    [SerializeField] private Button moveLeftButton;
    [SerializeField] private Button moveRightButton;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button interactAlternateButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAlternateText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pressToRebindingKey;


    public static OptionsUI Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        exitButton.onClick.AddListener(() =>
        {
            Hide();
        });
        moveUpButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Up); });
        moveDownButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Down); });
        moveLeftButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Left); });
        moveRightButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Right); });
        interactButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Interact); });
        interactAlternateButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Interact_Alternate); });
        pauseButton.onClick.AddListener(() => { Rebinding(GameInput.Binding.Pause); });
    }



    private void Start()
    {
        MyGameManager.Instance.OnGameUnpaused += MyGameManager_OnGameUnpaused;
        UpdateVisual();
        Hide();
        HidePressToRebindingKey();
    }

    private void MyGameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "SFX: " + Mathf.Round(SoundManager.Instance.GetVolume() * 10f);
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVolume() * 10f) ;

        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAlternateText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact_Alternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void ShowPressToRebindingKey()
    {
        pressToRebindingKey.gameObject.SetActive(true);
    }
    private void HidePressToRebindingKey()
    {
        pressToRebindingKey.gameObject.SetActive(false);
    }

    private void Rebinding(GameInput.Binding binding)
    {
        ShowPressToRebindingKey();
        GameInput.Instance.Rebinding(binding, () => 
        {
            HidePressToRebindingKey();
            UpdateVisual();
        }
        );
    }
}
