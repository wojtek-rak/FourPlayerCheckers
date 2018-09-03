using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field  {
    public PawnController pawnController { get; set; }
    public PlayerPosition playerPosition { get; set; }
    public bool Inaccessible { get; set; }
    public bool Free { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
}
