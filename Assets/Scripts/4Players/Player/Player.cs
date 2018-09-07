using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public enum PlayerPosition { Bottom, Upper, Left, Right, Empty }

    public abstract class Player : MonoBehaviour
    {

        public Object pwanPrefab;
        public List<PawnController> pawns = new List<PawnController>();

        //protected GameObject pawn => (GameObject)Instantiate(pwanPrefab, Vector3.zero, Quaternion.identity);
        protected PlayerPosition playerPosition;
        protected BoardController board;
        protected float fieldSize;
        protected float width;


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


        protected void SetUpColorPawns(Sprite sprite)
        {
            foreach (var pawn in pawns)
            {
                pawn.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

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