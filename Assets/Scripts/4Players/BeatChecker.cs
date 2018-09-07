using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Class for checking if beat is correct
    /// </summary>
    public class BeatChecker
    {
        /// <summary>First index in fields array, set if was beat</summary>
        public int BeatX { get; set; }
        /// <summary>Second index in fields array, set if was beat</summary>
        public int BeatY { get; set; }
        private BoardController board;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="_board">BoardController</param>
        public BeatChecker(BoardController _board)
        {
            board = _board;
        }
        /// <summary>
        /// Check if pawn with state Counter can beat
        /// </summary>
        /// <param name="startF"></param>
        /// <param name="endF"></param>
        /// <returns>True if beat is correct, or false</returns>
        public bool CanCounterBeat(Field startF, Field endF)
        {
            var maxX = Math.Max(startF.X, endF.X);
            var maxY = Math.Max(startF.Y, endF.Y);
            if (Math.Abs(startF.X - endF.X) == 2 && Math.Abs(startF.Y - endF.Y) == 2)
            {
                //check if between two fields is enemy
                if (board.fields[maxX - 1, maxY - 1].playerPosition != startF.playerPosition &&
                    board.fields[maxX - 1, maxY - 1].playerPosition != PlayerPosition.Empty &&
                    board.fields[maxX - 1, maxY - 1].Free == false)
                {
                    //chcek endF is free
                    if (endF.Free == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Check if pawn with state Counter can move
        /// </summary>
        /// <param name="startF"></param>
        /// <param name="endF"></param>
        /// <returns>True if move is correct, or false</returns>
        public bool CanCounterMove(Field startF, Field endF)
        {
            if (Math.Abs(startF.X - endF.X) == 1 && Math.Abs(startF.Y - endF.Y) == 1)
            {
                if (endF.Free == true)
                {
                    if ((startF.playerPosition == PlayerPosition.Upper && endF.Y > startF.Y) ||
                        (startF.playerPosition == PlayerPosition.Bottom && endF.Y < startF.Y)||
                        (startF.playerPosition == PlayerPosition.Right && endF.X < startF.X)||
                        (startF.playerPosition == PlayerPosition.Left && endF.X > startF.X))

                    {
                        return true;
                    }
                }
            }
            return false;
        }
        /// <summary>
        /// Check if pawn with state Queen can move
        /// </summary>
        /// <param name="startF"></param>
        /// <param name="endF"></param>
        /// <returns>True if move is correct, or false</returns>
        public bool CanQueenMove(Field startF, Field endF)
        {
            var signX = 1;
            var signY = 1;
            if (startF.X > endF.X) signX = -1;
            if (startF.Y > endF.Y) signY = -1;
            if (Math.Abs(startF.X - endF.X) == Math.Abs(startF.Y - endF.Y))
            {
                var count = Math.Abs(startF.X - endF.X);
                var it = 1;
                while (count >= it)
                {
                    if (board.fields[startF.X + it * signX, startF.Y + it * signY].Free != true) return false;
                    it++;
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Check if pawn with state Queen can beat
        /// </summary>
        /// <param name="startF"></param>
        /// <param name="endF"></param>
        /// <returns>True if beat is correct, or false</returns>
        public bool CanQueenBeat(Field startF, Field endF)
        {
            var signX = 1;
            var signY = 1;
            if (startF.X > endF.X) signX = -1;
            if (startF.Y > endF.Y) signY = -1;
            if (Math.Abs(startF.X - endF.X) == Math.Abs(startF.Y - endF.Y))
            {
                var counter = 0;
                var count = Math.Abs(startF.X - endF.X);
                var it = 1;
                while (count >= it)
                {
                    if (board.fields[startF.X + it * signX, startF.Y + it * signY].Free != true)
                    {
                        if (board.fields[startF.X + it * signX, startF.Y + it * signY].playerPosition == startF.playerPosition) return false;
                        if (board.fields[startF.X + it * signX, startF.Y + it * signY].playerPosition != startF.playerPosition &&
                            board.fields[startF.X + it * signX, startF.Y + it * signY].playerPosition != PlayerPosition.Empty )
                        {
                            BeatX = startF.X + it * signX;
                            BeatY = startF.Y + it * signY;
                            counter++;
                        }
                    }
                    it++;
                }
                if (counter == 1)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

