using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class PlayerUpperController : Player
    {
        // Use this for initialization
        protected override void Start()
        {
            playerPosition = PlayerPosition.Upper;
            base.Start();
            SetUpColorPawns(Resources.Load<Sprite>("player_2"));
        }

        protected override void SetUpPawns()
        {
            var count = 0;
            var row = 0;
            var col = 4;
            while (count < pawns.Count)
            {
                var pawn = pawns[count];
                if (board.fields[col, row].Free)
                {
                    board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                    board.fields[col, row].playerPosition = PlayerPosition.Upper;
                    board.fields[col, row].Free = false;
                    pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f,
                        width - row * fieldSize - fieldSize / 2f, 10f);
                    if (++col >= 12)
                    {
                        col = 4;
                        row += 1;
                    }
                    count += 1;
                }
                else
                {
                    if (++col >= 12)
                    {
                        col = 4;
                        row += 1;
                    }
                }
            }
        }

    }
}
