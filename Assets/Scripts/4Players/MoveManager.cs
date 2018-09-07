using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Class for checking and executing move.
    /// </summary>
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

        /// <summary>
        /// Set up
        /// </summary>
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
        /// <summary>
        /// Remember field on which pawn start move
        /// </summary>
        /// <param name="pawn">Pawn which moving</param>
        public void StartHolding(GameObject pawn)
        {
            startPosition = pawn.transform.localPosition;
            posStartX = board.CalculateFieldX(startPosition.x);
            posStartY = board.CalculateFieldY(startPosition.y);
        }
        /// <summary>
        /// Remember field on which pawn end move
        /// </summary>
        /// <remarks>
        /// Check if move is legal, and if so execute it, if not move pawn to start position.
        /// </remarks>
        /// <seealso cref="MoveManager.CanMove"/>
        /// <seealso cref="MoveManager.ExecuteMove(GameObject)"/>
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

        /// <summary>
        /// Check if move is possible
        /// </summary>
        /// <remarks>
        /// For Pawn with State.Counter it calls: BeatChecker.CanCounterBeat(startField, endField)
        /// or BeatChecker.CanCounterMove(startField, endField), based on how long is move,
        /// and active CheckQueen(startField, endField)
        /// For Pawn with State.Queen it calls: BeatChecker.CanQueenMove(startField, endField)
        /// if its true, method return true, if not it calls beatChecker.CanQueenBeat(startField, endField);
        /// Based on that it ser private values move, beat, beatX, beatY,
        /// </remarks>
        /// <returns>
        /// True: if move is possible,
        /// False: if move is not possible
        /// </returns>
        /// <seealso cref="CheckQueen(Field, Field)"/>
        /// <seealso cref="BeatChecker"/>
        /// <seealso cref="BeatChecker.CanCounterMove(Field, Field)"/>
        /// <seealso cref="BeatChecker.CanCounterBeat(Field, Field)"/>
        /// <seealso cref="BeatChecker.CanQueenMove(Field, Field)"/>
        /// <seealso cref="BeatChecker.CanQueenBeat(Field, Field)"/>
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
                    break;
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
                    break;
                default:
                    return false;
            }
            if (move)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Execute move
        /// </summary>
        /// <remarks>
        /// It is executed when CanMove return true, it set up start and end Field,
        /// if was beat, it call Kill() on beaten pawn. 
        /// If wasn't it call BeatOrDieChecker.CheckBeats(List{PawnController}),
        /// If CheckQueen returned true, it also call PawnController.TransformToQueen()
        /// </remarks>
        /// <param name="pawn">Pawn which is moving</param>
        /// <seealso cref="BeatOrDieChecker.CheckBeats(List{PawnController})"/>
        public void ExecuteMove(GameObject pawn)
        {
            var startField = board.fields[posStartX, posStartY];
            var endField = board.fields[posEndX, posEndY];

            TurnManager.NextTurn();
            if (beat)
            {
                board.fields[beatX, beatY].PawnController.Kill();
                TurnManager.turn = pawnController.playerPosition;
                beat = false;
            }
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
        /// <summary>
        /// Check if pawn is far enough for transform to queen
        /// </summary>
        /// <param name="startF">Field on which pawn start move</param>
        /// <param name="endF">Field on which pawn end move</param>
        public void CheckQueen(Field startF, Field endF)
        {
            switch (startF.playerPosition)
            {
                case PlayerPosition.Upper:
                    if (endF.Y == lastIndex || endF.X == firstIndex || endF.X == lastIndex) queen = true;
                    break;
                case PlayerPosition.Bottom:
                    if (endF.Y == firstIndex || endF.X == firstIndex || endF.X == lastIndex) queen = true;
                    break;
                case PlayerPosition.Right:
                    if (endF.Y == firstIndex || endF.Y == lastIndex || endF.X == firstIndex ) queen = true;
                    break;
                case PlayerPosition.Left:
                    if (endF.Y == firstIndex || endF.Y == lastIndex || endF.X == lastIndex) queen = true;
                    break;

            }
        }
    }

}


