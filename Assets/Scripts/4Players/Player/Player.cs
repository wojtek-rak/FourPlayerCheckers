using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Player enum
    /// </summary>
    public enum PlayerPosition {
        /// <summary>Bottom Player</summary>
        Bottom,
        /// <summary>Upper Player</summary>
        Upper,
        /// <summary>Left Player</summary>
        Left,
        /// <summary>Right Player</summary>
        Right,
        /// <summary>For empty fields</summary>
        Empty
    }
    /// <summary>
    /// Abstract Player class
    /// </summary>
    /// <remarks>
    /// Design for max 4 players
    /// </remarks>
    public abstract class Player : MonoBehaviour
    {
        /// <summary>
        /// Pawn prefab, same for all players, only what is change is sprite
        /// </summary>
        /// <seealso cref="SetUpColorPawns(Sprite)"/>
        public Object pwanPrefab;
        /// <summary>List of player's pawns</summary>
        public List<PawnController> pawns = new List<PawnController>();
        /// <summary>playerPosition represents position of player</summary>
        protected PlayerPosition playerPosition;
        /// <summary>Reference to BoardController</summary>
        protected BoardController board;
        /// <summary>High and width of field on screen</summary>
        protected float fieldSize;
        /// <summary>Width of board on screen</summary>
        protected float width;

        /// <summary>
        /// Initialize Player
        /// </summary>
        /// <remarks>
        /// Add 12 pawns to pawns List set them children of object which is on
        /// left-bottom corner on the board, and finally put them on board.
        /// </remarks>
        protected virtual void Start()
        {
            var tempBoard = GameObject.FindGameObjectWithTag("Board");
            var corner = GameObject.FindGameObjectWithTag("Corner").transform;
            board = tempBoard.GetComponent<BoardController>();
            width = 500f;// to do much better same as Board // tempBoard.GetComponent<RectTransform>().rect.width;
            fieldSize = width / 16f;
            pwanPrefab = Resources.Load("Pawn4");
            for (int i = 0; i < 12; i++)
            {
                var pawn = (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
                pawns.Add(pawn.GetComponent<PawnController>());
                pawns[i].playerPosition = playerPosition;
                pawn.transform.parent = corner;
            }
            SetUpPawns();
        }

        /// <summary>
        /// Method for change pawn's sprite.
        /// </summary>
        /// <param name="sprite">sprite for placement</param>
        protected void SetUpColorPawns(Sprite sprite)
        {
            foreach (var pawn in pawns)
            {
                pawn.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
        /// <summary>
        /// Abstract method, for set up position pawns on board
        /// </summary>
        protected abstract void SetUpPawns();

        ///// <summary>
        ///// testing
        ///// </summary>
        //private void Update()
        //{
        //    //<TESTING>
        //    if (!xd)
        //    {
        //        SetupEarlyQeenForSomeFun();
        //        xd = true;
        //    }

        //    //</TESTING?
        //}
        //private bool xd = false;
        ///// <summary>
        ///// TESTING METOD
        ///// </summary>
        //public void SetupEarlyQeenForSomeFun()
        //{
        //    if (playerPosition == PlayerPosition.Upper)
        //    {
        //        Debug.Log("upper done");
        //        board.fields[2, 2].PawnController.TransformToQueen();
        //        board.fields[2, 2].PawnController.state = State.Queen;
        //    }
        //    else
        //    {
        //        Debug.Log("bottom done");
        //        board.fields[1, 5].PawnController.TransformToQueen();
        //        board.fields[1, 5].PawnController.state = State.Queen;
        //    }

        //}
    }
}