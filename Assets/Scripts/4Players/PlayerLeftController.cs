using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class PlayerLeftController : Player
    {

        protected override void Start()
        {
            playerPosition = PlayerPosition.Left;
            base.Start();
        }

    }
}
