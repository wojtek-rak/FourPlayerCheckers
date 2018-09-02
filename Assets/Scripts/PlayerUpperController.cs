using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpperController : Player {

    // Use this for initialization
    protected override void Start ()
    {
        playerPosition = PlayerPosition.Upper;
        base.Start();
	}
	
	// Update is called once per frame
	void Update ()
    {
		//Debug.Log(pawns[2].state);
	}
    
}
