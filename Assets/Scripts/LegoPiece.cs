using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class LegoPiece : MonoBehaviour
{
    public int Index;

    public Rigidbody rb;

    public bool canSpawnMain;

    public bool isCollided;

    public bool isGrabbed;

    public bool canMerge;

    public float HeightY;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        //List<Transform> childs = new List<Transform>();
        //foreach (var child in transform)
        //{
        //    childs.Add((Transform)child);
        //}

        //var minY = childs.OrderBy(o => o.transform.localPosition.y).First().localPosition.y;
        //var maxY = childs.OrderBy(o => o.transform.localPosition.y).Last().localPosition.y;

        //HeightY = maxY - minY + childs[0].GetComponent<MeshRenderer>().bounds.size.y;
        //Debug.Log(HeightY + name);
    }

    

    //            var newPos = new Vector3(currontLego.transform.position.x, transform.position.y, currontLego.transform.position.z);

    //            transform.DOMove(newPos, 0.1f).OnComplete(() =>
    //            {

    //                LeagoPieceManager.Instance.SpawnNewLegoPieces(currontLego);
    //                Destroy(gameObject);
    //                Destroy(currontLego.gameObject);

    


}

