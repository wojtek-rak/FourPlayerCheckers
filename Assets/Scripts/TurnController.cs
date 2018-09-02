using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnController : MonoBehaviour {

    public static PlayerPosition turn;

    void Start () {
        turn = PlayerPosition.Bottom;
    }
	
    public void NextTurn()
    {
        if (turn == PlayerPosition.Bottom) turn = PlayerPosition.Upper;
        else turn = PlayerPosition.Bottom;
    }

}
