using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OneVsOne
{
    public class PositionController : MonoBehaviour
    {

        public GameObject board;
        // Use this for initialization
        void Start()
        {
            board = GameObject.FindGameObjectWithTag("Board");
        }

    }
}