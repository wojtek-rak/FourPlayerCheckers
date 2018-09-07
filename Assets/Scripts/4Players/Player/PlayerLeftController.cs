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
            SetUpColorPawns(Resources.Load<Sprite>("player_4"));
        }

        protected override void SetUpPawns()
        {
            var count = 0;
            var row = 4;
            var col = 0;
            while (count < pawns.Count)
            {
                var pawn = pawns[count];
                if (board.fields[col, row].Free)
                {
                    board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                    board.fields[col, row].Free = false;
                    board.fields[col, row].playerPosition = PlayerPosition.Left;
                    pawn.transform.localPosition = new Vector3(col * fieldSize + fieldSize / 2f,
                        width - row * fieldSize - fieldSize / 2f, 10f);
                    if (++row >= 12)
                    {
                        row = 4;
                        col += 1;
                    }
                    count += 1;
                }
                else
                {
                    if (++row >= 12)
                    {
                        row = 4;
                        col += 1;
                    }
                }
            }
        }
    }
}
