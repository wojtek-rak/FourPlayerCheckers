using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    /// <summary>
    /// Pawn state enum
    /// </summary>
    public enum State {
        /// <summary>Pawn is counter</summary>
        Counter,
        /// <summary>Pawn is queen</summary>
        Queen,
        /// <summary>Pawn is dead</summary>
        Dead
    }
    /// <summary>
    /// Class which controls board
    /// </summary>
    public class PawnController : MonoBehaviour
    {
        /// <summary>Actual pawn's state</summary>
        public State state;
        /// <summary>Panw belongs to that player</summary>
        public PlayerPosition playerPosition;
        /// <summary>Pawn is on that field</summary>
        public Field Field { get; set; }

        private MoveManager moveController;
        private bool dragging = false;
        private float distance;

        /// <summary>
        /// Set up pawn, set state to Counter
        /// </summary>
        void Start()
        {
            state = State.Counter;
            var gameManager = GameObject.FindGameObjectWithTag("GameManager");
            moveController = gameManager.GetComponent<MoveManager>();
        }
        /// <summary>
        /// Active when click-down on pawn
        /// it active MoveManager.StartHolding(gameObject)
        /// </summary>
        /// /// <seealso cref="MoveManager.StartHolding(GameObject)"/>
        void OnMouseDown()
        {
            if (TurnManager.turn == playerPosition)
            {
                moveController.StartHolding(gameObject);
                dragging = true;
            }
        }
        /// <summary>
        /// Active when click-up on pawn,
        /// it active MoveManager.StopHolding(gameObject)
        /// </summary>
        /// <seealso cref="MoveManager.StopHolding(GameObject)"/>
        void OnMouseUp()
        {
            if (TurnManager.turn == playerPosition)
            {
                moveController.StopHolding(gameObject);
                dragging = false;
            }
        }
        /// <summary>
        /// Method used on beated pawns, set pawn's state to Dead,
        /// unactive sprite, and move it behind board.
        /// </summary>
        public void Kill()
        {
            Field.Free = true;
            state = State.Dead;
            gameObject.GetComponent<Renderer>().enabled = false;
            var pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(pos.x, pos.y, 200f);

            // SAVE to gloobal list as point
        }

        /// <summary>
        /// Method for change sprite and state of pawn to queen
        /// </summary>
        public void TransformToQueen()
        {
            switch (playerPosition)
            {
                case PlayerPosition.Bottom:
                    state = State.Queen;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("player_2_q");
                    break;
                case PlayerPosition.Upper:
                    state = State.Queen;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("player_1_q");
                    break;
                case PlayerPosition.Right:
                    state = State.Queen;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("player_3_q");
                    break;
                case PlayerPosition.Left:
                    state = State.Queen;
                    gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("player_4_q");
                    break;
            }
        }

        /// <summary>
        /// Change position of pawn, while dragging.
        /// </summary>
        void Update()
        {
            if (dragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector3 rayPoint = ray.GetPoint(distance);
                transform.position = new Vector3(rayPoint.x, rayPoint.y, 20f);
            }
        }
    }
}