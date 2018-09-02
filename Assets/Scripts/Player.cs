using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{

    public Object pwanPrefab;
    protected GameObject pawn => (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
    public GameObject[] pawns = new GameObject[8];

    void Start()
    {
        Debug.Log("XD");
        //pwanPrefab = Resources.Load("Assets/Prefabs/Pawn");
        Debug.Log(pwanPrefab);
        for (int i = 0; i < 8; i++)
        {
            pawns[i] = pawn;
        }
    }
    //protected abstract void SetUpPawns();

}
