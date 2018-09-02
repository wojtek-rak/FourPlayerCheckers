using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerPosition { Upper, Bottom}

public class Player : MonoBehaviour{

    public Object pwanPrefab;
    public List<PawnController> pawns = new List<PawnController>();

    //protected GameObject pawn => (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
    protected PlayerPosition playerPosition;

    private BoardController board;
    private float fieldSize;
    private float width;


    protected virtual void Start()
    {
        var tempBoard = GameObject.FindGameObjectWithTag("Board");
        var corner = GameObject.FindGameObjectWithTag("Corner").transform;
        board = tempBoard.GetComponent<BoardController>();
        width = 330f;// to do much better // tempBoard.GetComponent<RectTransform>().rect.width;
        fieldSize = width / 8f;
        pwanPrefab = Resources.Load("Pawn");
        Debug.Log(pwanPrefab);
        for (int i = 0; i < 12; i++)
        {
            var pawn = (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
            pawns.Add(pawn.GetComponent<PawnController>());
            pawns[i].playerPosition = playerPosition;
            pawn.transform.parent = corner;
        }
        SetUpPawns();
    }
    private void SetUpPawns()
    {
        Debug.Log(fieldSize);
        var count = 0;
        var row = 0;
        var col = 0;
        switch(playerPosition)
        {
            case PlayerPosition.Upper:
                while(count < pawns.Capacity)
                {
                    var pawn = pawns[count];
                    if (board.fields[col, row].Free)
                    {
                        board.fields[col, row].Free = false;
                        pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f, width - row * fieldSize - fieldSize / 2f, 10f);
                        Debug.Log("second");
                        if (++col >= 8)
                        {
                            col = 0;
                            row += 1;
                        }
                        Debug.Log(col);
                        count += 1;
                    }
                    else
                    {
                        if (++col >= 8)
                        {
                            col = 0;
                            row += 1;
                        }
                    }
                }
                break;
            case PlayerPosition.Bottom:

                break;
        }
        

        //foreach(var pawn in pawns)
        //{
        //    pawn.transform.position
        //}
    }

}
