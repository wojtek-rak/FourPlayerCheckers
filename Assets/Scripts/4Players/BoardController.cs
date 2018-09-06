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
                        fields[i, j].Inaccessible = false;
                        fields[i, j].Free = true;
                        fields[i, j].X = i;
                        fields[i, j].Y = j;
                    }
                    else if (i % 2 == 1 && j % 2 == 1)
                    {
                        fields[i, j].Inaccessible = false;
                        fields[i, j].Free = true;
                        fields[i, j].X = i;
                        fields[i, j].Y = j;
                    }
                    else
                    {
                        fields[i, j].Inaccessible = true;
                        fields[i, j].Free = false;
                        fields[i, j].X = i;
                        fields[i, j].Y = j;
                    }
                    
                    if ((i >= rightBCorner && j >= rightBCorner) ||
                        (i <= leftUCorenr && j <= leftUCorenr) ||
                        (i <= leftUCorenr && j >= rightBCorner) ||
                        (i >= rightBCorner && j <= leftUCorenr))
                    {
                        
                        fields[i, j].Inaccessible = true;
                        fields[i, j].Free = false;
                        fields[i, j].X = i;
                        fields[i, j].Y = j;
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
    }
}