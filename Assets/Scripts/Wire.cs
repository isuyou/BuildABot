using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class Wire : MonoBehaviour  {
	bool state = false;
	Animator anim;
	// Use this for initialization
	void Start () {

		anim = GetComponent<Animator> ();
	}

	void ChangeLight(bool state){
		anim.SetBool ("win", state);
	}

	// Update is called once per frame
	void Update () {
		if (!state && CircuitController.controller.win) {
			state = true;
			ChangeLight (true);
		}
	}
}
