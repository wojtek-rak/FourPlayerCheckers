using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {

    private Player[] players = new Player[2];
    private readonly int upperPlayer = (int)PlayerPosition.Upper;
    private readonly int bottomPlayer = (int) PlayerPosition.Bottom;
    private BoardController board;

    private PawnController pawnController;
    private Vector3 startPosition;
    private int posStartX;
    private int posStartY;
    private int posEndX;
    private int posEndY;

    void Start () {
        var gameManager = GameObject.FindGameObjectWithTag("GameManager");
        players[upperPlayer] = gameManager.GetComponent<PlayerUpperController>();
        players[bottomPlayer] = gameManager.GetComponent<PlayerBottomController>();
        board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
    }
	
    public void StartHolding(GameObject pawn)
    {
        startPosition = pawn.transform.localPosition;
        posStartX = board.CalculateFieldX(startPosition.x);
        posStartY = board.CalculateFieldY(startPosition.y);
    }
    public void StopHolding(GameObject pawn)
    {
        var pos = pawn.transform.localPosition;
        posEndX = board.CalculateFieldX(pos.x);
        posEndY = board.CalculateFieldY(pos.y);
        pawnController = pawn.GetComponent<PawnController>();
        if (CanMove(pawnController))
        {
            
        }
        else
        {
            pawn.transform.localPosition = startPosition;
        }
    }

    public bool CanMove(PawnController pawn)
    {
        switch(pawnController.state)
        {
            case State.Counter:
                if(Math.Abs(posStartX - posEndX) > 1)
                {
                    //CanBeat
                }
                else
                {

                }
                return false;
                break;
            case State.Queen:
                return false;
                break;
            default:
                return false;
        }
    }

}
