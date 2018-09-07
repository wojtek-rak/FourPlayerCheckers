using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class PlayerBottomController : Player
    {

        protected override void Start()
        {
            playerPosition = PlayerPosition.Bottom;
            base.Start();
            SetUpColorPawns(Resources.Load<Sprite>("player_1"));
        }

        protected override void SetUpPawns()
        {
            var count = 0;
            var row = 15;
            var col = 11;
            while (count < pawns.Count)
            {
                var pawn = pawns[count];
                if (board.fields[col, row].Free)
                {
                    board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                    board.fields[col, row].Free = false;
                    board.fields[col, row].playerPosition = PlayerPosition.Bottom;
                    pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f,
                        width - row * fieldSize - fieldSize / 2f, 10f);
                    if (--col <= 3)
                    {
                        col = 11;
                        row -= 1;
                    }
                    count += 1;
                }
                else
                {
                    if (--col <= 3)
                    {
                        col = 11;
                        row -= 1;
                    }
                }
            }
        }
    }
}
