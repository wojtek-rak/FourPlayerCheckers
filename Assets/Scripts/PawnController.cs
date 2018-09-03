using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Counter, Queen, Dead}
public class PawnController : MonoBehaviour {

    public State state;
    public PlayerPosition playerPosition;

    private MoveController moveController;
    private bool dragging = false;
    private float distance;

    void Start () {
        state = State.Counter;
        var gameManager = GameObject.FindGameObjectWithTag("GameManager");
        moveController = gameManager.GetComponent<MoveController>();
    }

    void OnMouseDown()
    {
        if (TurnController.turn == playerPosition)
        {
            moveController.StartHolding(gameObject);
            dragging = true;
        }
    }

    void OnMouseUp()
    {
        if (TurnController.turn == playerPosition)
        {
            moveController.StopHolding(gameObject);
            dragging = false;
        }
    }

    public void Kill()
    {
        state = State.Dead;
        gameObject.GetComponent<Renderer>().enabled = false;
        var pos = gameObject.transform.localPosition;
        gameObject.transform.localPosition = new Vector3(pos.x, pos.y, 200f);
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
