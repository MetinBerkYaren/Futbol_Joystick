using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<GameObject> Levels = new List<GameObject>();

    public static LevelManager Instance;

    
    private void Awake()
    {

        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        Initiliaze();
    }

    public void Initiliaze()
    {
        var levelIndex = DataManager.CurrentLevel % Levels.Count;

        Instantiate(Levels[levelIndex]);

        //var levelIndex = DataManager.CurrentLevel % Levels.Count;

        //Instantiate(Levels[levelIndex]);

    }
}