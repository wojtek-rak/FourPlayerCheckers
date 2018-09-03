using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {

    public static PlayerPosition turn;

    void Start () {
        turn = PlayerPosition.Bottom;
    }
	
    public static void NextTurn()
    {
        if (turn == PlayerPosition.Bottom) turn = PlayerPosition.Upper;
        else turn = PlayerPosition.Bottom;
    }

    public static PlayerPosition GetOppositePosition(PlayerPosition pos)
    {
        if (pos == PlayerPosition.Bottom) return PlayerPosition.Upper;
        else return PlayerPosition.Bottom;
    }

}
