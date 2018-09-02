using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Counter, Queen, Dead}
public class PawnController : MonoBehaviour {

    public State state;
    public PlayerPosition playerPosition;
	// Use this for initialization
	void Start () {
        state = State.Counter;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
