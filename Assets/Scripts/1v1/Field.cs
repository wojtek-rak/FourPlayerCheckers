using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneVsOne
{
    public class Field
    {
        private PawnController pawnController;
        public PawnController PawnController
        {
            get
            {
                return pawnController;
            }
            set
            {
                pawnController = value;
                if (value != null) value.Field = this;
            }
        }
        public PlayerPosition playerPosition { get; set; }
        public bool Inaccessible { get; set; }
        public bool Free { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}