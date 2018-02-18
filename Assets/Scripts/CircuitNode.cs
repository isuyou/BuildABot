using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircuitNode : MonoBehaviour {

    public int x;
    public int y;
    public GameObject CircuitBoard;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Input.GetMouseButtonDown(0);
        //do somethign
	}
}
