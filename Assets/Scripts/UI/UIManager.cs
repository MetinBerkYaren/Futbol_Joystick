using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UIGameStart UIGameStart;
    public UIGamePlay UIGamePlay;
    public UIGameEnd UIGameEnd;

    private float gameEndDelayTime = 2f;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        UIGameStart.gameObject.SetActive(true);
        UIGamePlay.gameObject.SetActive(false);
        UIGameEnd.gameObject.SetActive(false);

    }

    private void Start()
    {
        GamePlayManager.Instance.OnGameStart += GameStarted;
        GamePlayManager.Instance.OnLevelEnd += GameEnded;
    }

    private void GameStarted()
    {
        UIGameStart.gameObject.SetActive(false);
        UIGamePlay.gameObject.SetActive(true);
    }

    private void GameEnded(bool isWin)
    {
        StartCoroutine(DelayGameEndPage());

        IEnumerator DelayGameEndPage()
        {
            UIGamePlay.gameObject.SetActive(false);
            yield return new WaitForSeconds(gameEndDelayTime);
            UIGameEnd.gameObject.SetActive(true);
            UIGameEnd.Initialize(isWin);
        }

    }

   // public void NextButtonClick()
   // {
   //     GamePlayManager.Instance.LoadMainMenu(true);
   // }
   //
   // public void RetryButtonClicked()
   // {
   //     GamePlayManager.Instance.LoadMainMenu(false);
   // }
}