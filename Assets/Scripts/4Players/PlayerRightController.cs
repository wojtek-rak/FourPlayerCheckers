using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class PlayerRightController : Player
    {

        protected override void Start()
        {
            playerPosition = PlayerPosition.Right;
            base.Start();
        }

    }

}