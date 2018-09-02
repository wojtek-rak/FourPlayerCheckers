using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerBottomController : Player
{

    protected override void Start()
    {
        playerPosition = PlayerPosition.Bottom;
        base.Start();
    }

}

