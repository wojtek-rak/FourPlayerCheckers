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
            SetUpColorPawns(Resources.Load<Sprite>("player_3"));
        }

        protected override void SetUpPawns()
        {
            var count = 0;
            var row = 4;
            var col = 13;
            while (count < pawns.Count)
            {
                var pawn = pawns[count];
                if (board.fields[col, row].Free)
                {
                    board.fields[col, row].PawnController = pawn.GetComponent<PawnController>();
                    board.fields[col, row].Free = false;
                    board.fields[col, row].playerPosition = PlayerPosition.Right;
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