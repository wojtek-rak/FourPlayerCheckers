using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public class BoardController : MonoBehaviour
    {
        public GameObject pawn;
        public Field[,] fields = new Field[16, 16];
        public float fieldSize;

        // todo better same as player
        private float width = 500f;
        // >= =<
        private const int rightBCorner = 12;
        private const int leftUCorenr = 3;
        private const int longV = 2;

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

        public int CalculateFieldX(float range)
        {
            var index = range / fieldSize;
            return (int)index;
        }
        public int CalculateFieldY(float range)
        {
            var index = (width - range) / fieldSize;
            return (int)index;
        }

        public Vector3 CalculatePosition(int x, int y)
        {
            return new Vector3(x * fieldSize + fieldSize / 2f, width - y * fieldSize - fieldSize / 2f, 10f);
        }
        
        private void SetUpField(int x, int y, bool free)
        {
            fields[x, y].Free = free;
            fields[x, y].X = x;
            fields[x, y].Y = y;
        }
    }
}