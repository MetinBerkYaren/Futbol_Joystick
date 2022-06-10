//using GameAnalyticsSDK;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GamePlayManager : MonoBehaviour
{
    public static GamePlayManager Instance;
  
    public UnityAction OnGameStart;
    public UnityAction<bool> OnLevelEnd;

    public bool isGameStarted;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }


    public void GameStarted()
    {
        isGameStarted = true;
        OnGameStart?.Invoke();
        //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, string.Format("Level {0}", DataManager.CurrentLevel));
    }

    public void LevelCompleted()
    {
        OnLevelEnd?.Invoke(true);
    }

    public void LevelFailed()
    {
        OnLevelEnd?.Invoke(false);

    } 

    public void LoadMainMenu(bool isWin = true)
    {
        if (isWin)
        {
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, string.Format("Level {0}", DataManager.CurrentLevel));

            DataManager.CurrentLevel++;
        }
        else
        {
            //GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, string.Format("Level {0}", DataManager.CurrentLevel));
        }

       

        SceneManager.LoadScene("GameScene");
    }
}