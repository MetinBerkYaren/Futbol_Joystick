using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class TempListClass
{
    public string name;
    public List<GameObject> Legos = new List<GameObject>();
}
public class LeagoPieceManager : MonoBehaviour
{
    public static LeagoPieceManager Instance;

    public List<TempListClass> LegoList = new List<TempListClass>();

    private void Awake()
    {
        if (Instance)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    public void SpawnNewLegoPieces(LegoPiece currentLego)
    {
        //Instantiate point
        var spawnedLego = Instantiate(LegoList[DataManager.CurrentLevel].Legos[currentLego.Index],
        new  Vector3(currentLego.transform.position.x, currentLego.transform.position.y + 0.1f, currentLego.transform.position.z),
        currentLego.transform.rotation);

        //Final object movement
        if (LegoList[DataManager.CurrentLevel].Legos.Count - 1 == currentLego.Index)
        {
            spawnedLego.transform.DOMove(new Vector3(0f, 0f, -1.9f), 1f).OnComplete(GamePlayManager.Instance.LevelCompleted);
        }

    }

}
