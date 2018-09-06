using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class MoveManager : MonoBehaviour
    {

        private Player[] players = new Player[4];
        private readonly int upperPlayer = (int)PlayerPosition.Upper;
        private readonly int bottomPlayer = (int)PlayerPosition.Bottom;
        private readonly int leftPlayer = (int)PlayerPosition.Left;
        private readonly int rightPlayer = (int)PlayerPosition.Right;
        private BoardController board;
        private BeatOrDieChecker beatOrDieChecker;

        private PawnController pawnController;
        private Vector3 startPosition;
        private int posStartX;
        private int posStartY;
        private int posEndX;
        private int posEndY;
        private int beatX;
        private int beatY;
        private const int lastIndex = 15;
        private const int firstIndex = 0;
        private bool move = false;
        private bool queen = false;
        private bool beat = false;
        private BeatChecker beatChecker;

        void Start()
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameManager");
            players[upperPlayer] = gameManager.GetComponent<PlayerUpperController>();
            players[bottomPlayer] = gameManager.GetComponent<PlayerBottomController>();
            players[leftPlayer] = gameManager.GetComponent<PlayerLeftController>();
            players[rightPlayer] = gameManager.GetComponent<PlayerRightController>();
            board = GameObject.FindGameObjectWithTag("Board").GetComponent<BoardController>();
            beatChecker = new BeatChecker(board);
            beatOrDieChecker = new BeatOrDieChecker(beatChecker, board);
        }

        public void StartHolding(GameObject pawn)
        {
            startPosition = pawn.transform.localPosition;
            posStartX = board.CalculateFieldX(startPosition.x);
            posStartY = board.CalculateFieldY(startPosition.y);
        }
        public void StopHolding(GameObject pawn)
        {
            var pos = pawn.transform.localPosition;
            posEndX = board.CalculateFieldX(pos.x);
            posEndY = board.CalculateFieldY(pos.y);
            pawnController = pawn.GetComponent<PawnController>();
            if (CanMove())
            {
                ExecuteMove(pawn);
            }
            else
            {
                pawn.transform.localPosition = startPosition;
            }
            queen = false;
            move = false;
            beat = false;
        }

        public bool CanMove()
        {
            var startField = board.fields[posStartX, posStartY];
            if (posEndX > lastIndex || posEndX < firstIndex || posEndY > lastIndex || posEndY < firstIndex)
            {
                return false;
            }
            var endField = board.fields[posEndX, posEndY];
            switch (pawnController.state)
            {
                case State.Counter:
                    CheckQueen(startField, endField);
                    if (Math.Abs(posStartX - posEndX) > 1)
                    {
                        if (beatChecker.CanCounterBeat(startField, endField))
                        {
                            move = true;
                            beat = true;
                            beatX = Math.Max(startField.X, endField.X) - 1;
                            beatY = Math.Max(startField.Y, endField.Y) - 1;
                        }
                    }
                    else if (beatChecker.CanCounterMove(startField, endField))
                    {
                        move = true;
                    }
                    if (move)
                    {
                        return true;
                    }
                    return false;

                case State.Queen:
                    Debug.Log("QUEEN MOVE");
                    if (beatChecker.CanQueenMove(startField, endField)) move = true;
                    else if (beatChecker.CanQueenBeat(startField, endField))
                    {
                        move = true;
                        beat = true;
                        beatX = beatChecker.BeatX;
                        beatY = beatChecker.BeatY;
                    }

                    if (move)
                    {
                        return true;
                    }
                    return false;
                default:
                    return false;
            }
        }


        public void ExecuteMove(GameObject pawn)
        {
            var startField = board.fields[posStartX, posStartY];
            var endField = board.fields[posEndX, posEndY];



            TurnManager.NextTurn();
            if (beat)
            {
                board.fields[beatX, beatY].PawnController.Kill();
                // beat give you next turn
                TurnManager.turn = pawnController.playerPosition;
                beat = false;
            }
            // if you could beat, but you didn't, you lose all pawns that could beat
            else
            {
                var pawnsToDie = beatOrDieChecker.CheckBeats(players[(int)startField.playerPosition].pawns);
                foreach (var pawnToKill in pawnsToDie)
                {
                    if (pawnToKill != pawnController)
                    {
                        pawnToKill.Kill();
                    }
                }
            }

            startField.Free = true;
            startField.playerPosition = PlayerPosition.Empty;
            startField.PawnController = null;
            endField.Free = false;
            endField.playerPosition = pawnController.playerPosition;
            endField.PawnController = pawnController;

            if (queen)
            {
                pawnController.TransformToQueen();
            }

            pawn.transform.localPosition = board.CalculatePosition(posEndX, posEndY);
        }

        public void CheckQueen(Field startF, Field endF)
        {
            switch (startF.playerPosition)
            {
                case PlayerPosition.Upper:
                    if (endF.Y == lastIndex) queen = true;
                    break;
                case PlayerPosition.Bottom:
                    if (endF.Y == firstIndex) queen = true;
                    break;
            }
        }
    }

}


