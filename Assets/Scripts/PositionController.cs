﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionController : MonoBehaviour {

    public GameObject board;
	// Use this for initialization
	void Start () {
        board = GameObject.FindGameObjectWithTag("Board");
        Debug.Log(board.GetComponent<RectTransform>().rect.width);
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}