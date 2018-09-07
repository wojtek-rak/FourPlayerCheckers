using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class TurnManager : MonoBehaviour
    {
        public static PlayerPosition turn;

        void Start()
        {
            turn = PlayerPosition.Bottom;
        }

        public static void NextTurn()
        {
            switch(turn)
            {
                case PlayerPosition.Bottom:
                    turn = PlayerPosition.Right;
                    break;
                case PlayerPosition.Right:
                    turn = PlayerPosition.Upper;
                    break;
                case PlayerPosition.Upper:
                    turn = PlayerPosition.Left;
                    break;
                case PlayerPosition.Left:
                    turn = PlayerPosition.Bottom;
                    break;
            }

        }

    }
}

