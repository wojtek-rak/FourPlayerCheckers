using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class BeatOrDieChecker
    {

        private const int maxIndex = 15;
        private BoardController board;
        private BeatChecker beatChecker;
        public BeatOrDieChecker(BeatChecker _beatChecker, BoardController _board)
        {
            beatChecker = _beatChecker;
            board = _board;
        }

        public List<PawnController> CheckBeats(List<PawnController> pawns)
        {
            List<PawnController> pawnsToKill = new List<PawnController>();

            foreach (var pawn in pawns)
            {
                switch (pawn.state)
                {
                    case State.Dead:
                        break;
                    case State.Counter:
                        if (NumberNotBig(pawn.Field.X) && NumberNotBig(pawn.Field.Y))
                        {
                            if (beatChecker.CanCounterBeat(pawn.Field, board.fields[pawn.Field.X + 2, pawn.Field.Y + 2]))
                            {
                                pawnsToKill.Add(pawn);
                                break;
                            }
                        }
                        if (NumberNotBig(pawn.Field.X) && NumberNotSmall(pawn.Field.Y))
                        {
                            if (beatChecker.CanCounterBeat(pawn.Field, board.fields[pawn.Field.X + 2, pawn.Field.Y - 2]))
                            {
                                pawnsToKill.Add(pawn);
                                break;
                            }
                        }
                        if (NumberNotSmall(pawn.Field.X) && NumberNotBig(pawn.Field.Y))
                        {
                            if (beatChecker.CanCounterBeat(pawn.Field, board.fields[pawn.Field.X - 2, pawn.Field.Y + 2]))
                            {
                                pawnsToKill.Add(pawn);
                                break;
                            }
                        }
                        if (NumberNotSmall(pawn.Field.X) && NumberNotSmall(pawn.Field.Y))
                        {
                            if (beatChecker.CanCounterBeat(pawn.Field, board.fields[pawn.Field.X - 2, pawn.Field.Y - 2]))
                            {
                                pawnsToKill.Add(pawn);
                                break;
                            }
                        }
                        break;
                    case State.Queen:
                        var breakC = false;
                        var signX = 1;
                        var signY = 1;
                        var range = SetUpRange();
                        var it = 0;
                        while (it <= 3)
                        {
                            var count = 1;

                            while (count <= maxIndex)
                            {
                                int? x = range[it][0];
                                int? y = range[it][1];
                                if (range[it][0] == 0) if (pawn.Field.X - count < 0) x = null;
                                if (range[it][0] == maxIndex) if (pawn.Field.X + count > maxIndex) x = null;
                                if (range[it][1] == 0) if (pawn.Field.Y + count > 0) y = null;
                                if (range[it][1] == maxIndex) if (pawn.Field.Y + count > maxIndex) y = null;

                                if (x != null && y != null)
                                {
                                    if (range[it][0] == 0) signX = -1;
                                    else signX = 1;
                                    if (range[it][1] == 0) signY = -1;
                                    else signY = 1;

                                    if (beatChecker.CanQueenBeat(pawn.Field, board.fields[pawn.Field.X + count * signX, pawn.Field.Y + count * signY]))
                                    {

                                        pawnsToKill.Add(pawn);
                                        breakC = true;
                                        break;
                                    }
                                }
                                count++;

                            }
                            if (breakC)
                            {
                                break;
                            }
                            it++;
                        }
                        break;
                }
            }
            return pawnsToKill;
        }


        private bool NumberNotBig(int r)
        {
            if (r > 13) return false;
            return true;
        }
        private bool NumberNotSmall(int r)
        {
            if (r < 2) return false;
            return true;
        }
        private int[][] SetUpRange()
        {
            var maxIndex = 15;
            int[][] range = new int[4][];
            range[0] = new int[2];
            range[1] = new int[2];
            range[2] = new int[2];
            range[3] = new int[2];

            range[0][0] = maxIndex;
            range[0][1] = maxIndex;

            range[1][0] = 0;
            range[1][1] = maxIndex;

            range[2][0] = maxIndex;
            range[2][1] = 0;

            range[3][0] = 0;
            range[3][1] = 0;
            return range;
        }
    }
}
