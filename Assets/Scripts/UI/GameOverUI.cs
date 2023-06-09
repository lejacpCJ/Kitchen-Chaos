using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        MyGameManager.Instance.OnStateChanged += MyGameManager_OnStateChanged;
        Hide();
    }
    private void MyGameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (MyGameManager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = Mathf.Ceil(DeliveryManager.Instance.GetSuccessfulRecipesAmount()).ToString();
        }
        else
        {
            Hide();
        }
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
