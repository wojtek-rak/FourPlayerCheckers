using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Class for manage turns
    /// </summary>
    public class TurnManager : MonoBehaviour
    {
        /// <summary>Actual player turn</summary>
        public static PlayerPosition turn;

        void Start()
        {
            turn = PlayerPosition.Bottom;
        }
        /// <summary>
        /// Method for change turn for next player
        /// </summary>
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

