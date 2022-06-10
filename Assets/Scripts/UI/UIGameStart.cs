using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameStart : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI levelNoText;
    
    // Start is called before the first frame update
    void Start()
    {
        
        SetUI();
    }

    private void SetUI()
    {
        levelNoText.text = string.Format("Level {0}", (DataManager.CurrentLevel + 1));
    }

    public void OnStartButtonTap()
    {
        GamePlayManager.Instance.GameStarted();
        
    }

    
}
