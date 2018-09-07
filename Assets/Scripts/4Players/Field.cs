using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Field on board class
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Property PawnController with get and set,
        /// after set non null PawnController, it calls:
        /// value.Field = this
        /// </summary>
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
        /// <summary>PlayerPosition Property</summary>
        public PlayerPosition playerPosition { get; set; }
        /// <summary>Free Property inform if field is occupied</summary>
        public bool Free { get; set; }
        /// <summary>First index in 2d fields array</summary>
        public int X { get; set; }
        /// <summary>SEcondindex in 2d fields array</summary>
        public int Y { get; set; }
        private PawnController pawnController;
    }
}