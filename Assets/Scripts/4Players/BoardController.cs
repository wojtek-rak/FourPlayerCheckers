using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Class which controls board
    /// </summary>
    public class BoardController : MonoBehaviour
    {
        /// <summary>2D array of Fields</summary>
        /// <seealso cref="Field"/>
        public Field[,] fields = new Field[16, 16];
        /// <summary>High and width of field on screen</summary>
        public float fieldSize;

        // todo better same as player
        private float width = 500f;
        private const int rightBCorner = 12;
        private const int leftUCorenr = 3;
        private const int longV = 2;


        /// <summary>
        /// Set up fields array, each field where pawn can be has Property Free set to true,
        /// except of 4x4 corners, which are not free.
        /// </summary>
        void Awake()
        {
            fieldSize = width / 16f;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    fields[i, j] = new Field();
                    fields[i, j].playerPosition = PlayerPosition.Empty;
                    if (i % 2 == 0 && j % 2 == 0)
                    {
                        SetUpField(i, j, true);
                    }
                    else if (i % 2 == 1 && j % 2 == 1)
                    {
                        SetUpField(i, j, true);
                    }
                    else
                    {
                        SetUpField(i, j, false);
                    }
                    
                    if ((i >= rightBCorner && j >= rightBCorner) ||
                        (i <= leftUCorenr && j <= leftUCorenr) ||
                        (i <= leftUCorenr && j >= rightBCorner) ||
                        (i >= rightBCorner && j <= leftUCorenr))
                    {
                        SetUpField(i, j, false);
                    }
                }
            }
        }
        /// <summary>
        /// Calculate index based on coordinate X 
        /// </summary>
        /// <param name="range">pawn.transform.localPosition.x</param>
        /// <returns>first index of pawn in pawns arrays</returns>
        public int CalculateFieldX(float range)
        {
            var index = range / fieldSize;
            return (int)index;
        }
        /// <summary>
        /// Calculate index based on coordinate Y 
        /// </summary>
        /// <param name="range">pawn.transform.localPosition.y</param>
        /// <returns>second index of pawn in pawns arrays</returns>
        public int CalculateFieldY(float range)
        {
            var index = (width - range) / fieldSize;
            return (int)index;
        }
        /// <summary>
        /// Calculate coordinates X and Y based on indexes
        /// </summary>
        /// <param name="x">First index in pawns array</param>
        /// <param name="y">Second index in pawns array</param>
        /// <returns>Vector3 of position</returns>
        public Vector3 CalculatePosition(int x, int y)
        {
            return new Vector3(x * fieldSize + fieldSize / 2f, width - y * fieldSize - fieldSize / 2f, 10f);
        }
        /// <summary>
        /// Set properties of Field
        /// </summary>
        /// <param name="x">first index</param>
        /// <param name="y">second index</param>
        /// <param name="free">value of property Free</param>
        private void SetUpField(int x, int y, bool free)
        {
            fields[x, y].Free = free;
            fields[x, y].X = x;
            fields[x, y].Y = y;
        }
    }
}