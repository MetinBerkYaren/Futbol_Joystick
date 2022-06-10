using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIGameEnd : MonoBehaviour
{
    public GameObject SuccessUI, FailUI;


    public void Initialize(bool isWin)
    {
        if (isWin)
        {
            InitializeSuccessUI();
        }
        else
        {
            InitializeFailUI();
        }
    }

    private void InitializeSuccessUI()
    {
        SuccessUI.SetActive(true);
        FailUI.SetActive(false);
    }

    private void InitializeFailUI()
    {
        FailUI.SetActive(true);
        SuccessUI.SetActive(false);
    }

    public void OnNextButtonTap()
    {
        //UIManager.Instance.NextButtonClick();
        GamePlayManager.Instance.LoadMainMenu(true);
    }

    public void OnRetryButtonTap()
    {
       // UIManager.Instance.RetryButtonClicked();
        GamePlayManager.Instance.LoadMainMenu(false);
    }
}
