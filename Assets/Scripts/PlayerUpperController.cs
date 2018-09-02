using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerUpperController : Player
{

    // Use this for initialization
    protected override void Start()
    {
        playerPosition = PlayerPosition.Upper;
        base.Start();
    }


}

