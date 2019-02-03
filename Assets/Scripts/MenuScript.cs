using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour {



	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump") || Input.GetButtonDown("JumpJerry")){
			Application.LoadLevel(1);
		}
	}

	public void StartGame(){
		Application.LoadLevel(1);
	}
}
