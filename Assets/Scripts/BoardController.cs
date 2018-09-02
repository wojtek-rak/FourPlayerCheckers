﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {
    public GameObject pawn;
    public Field[,] fields = new Field[8,8];

    // todo better same as player
    private float width = 330f;
    private float fieldSize;
    
    void Awake() {
        fieldSize = width / 8f;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                fields[i, j] = new Field();

                if (i % 2 == 0 && j % 2 == 0)
                {
                    fields[i, j].Inaccessible = false;
                    fields[i, j].Free = true;
                }
                else if (i % 2 == 1 && j % 2 == 1)
                {
                    fields[i, j].Inaccessible = false;
                    fields[i, j].Free = true;
                }
                else
                {
                    fields[i, j].Inaccessible = true;
                    fields[i, j].Free = false;
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
}
