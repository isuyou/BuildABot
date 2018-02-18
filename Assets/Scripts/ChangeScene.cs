using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {
	private IEnumerator coroutine;

	public static void switchScene(string sceneName){
		SceneManager.LoadScene("sceneName",LoadSceneMode.Single);
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
