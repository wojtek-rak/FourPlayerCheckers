﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourPlayers
{
    public enum State { Counter, Queen, Dead }
    public class PawnController : MonoBehaviour
    {

        public State state;
        public PlayerPosition playerPosition;
        public Field Field { get; set; }

        private MoveManager moveController;
        private bool dragging = false;
        private float distance;

        void Start()
        {
            state = State.Counter;
            var gameManager = GameObject.FindGameObjectWithTag("GameManager");
            moveController = gameManager.GetComponent<MoveManager>();
        }

        void OnMouseDown()
        {
            if (TurnManager.turn == playerPosition)
            {
                moveController.StartHolding(gameObject);
                dragging = true;
            }
        }

        void OnMouseUp()
        {
            if (TurnManager.turn == playerPosition)
            {
                moveController.StopHolding(gameObject);
                dragging = false;
            }
        }

        public void Kill()
        {
            Field.Free = true;
            state = State.Dead;
            gameObject.GetComponent<Renderer>().enabled = false;
            var pos = gameObject.transform.localPosition;
            gameObject.transform.localPosition = new Vector3(pos.x, pos.y, 200f);

            // SAVE to gloobal list as point
        }

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
                    Debug.Log("TODO");
                    break;
                case PlayerPosition.Left:
                    Debug.Log("TODO");
                    break;
            }
        }

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