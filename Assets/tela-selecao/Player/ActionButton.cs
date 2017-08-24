using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {

	private GameObject StartScene;

	public void cancel(){
		Debug.Log ("Cancel");
		StartScene = transform.parent.GetComponent<StartScene>().nextScene;

		GameObject teste = GameObject.Find("newPlayer");
		teste.GetComponent<Animator> ().enabled = true;	//Parar a animação
		teste.GetComponent<PlayerMovement>().speed = 4.0f;	//Para a movimentação do personagem
		StartScene.SetActive (false);

	}
	public void ok(){
		Debug.Log ("Ok.");
		StartScene = transform.parent.GetComponent<StartScene>().nextScene;

		GameObject teste = GameObject.Find("newPlayer");
		teste.GetComponent<Animator> ().enabled = true;	//Parar a animação
		teste.GetComponent<PlayerMovement>().speed = 4.0f;	//Para a movimentação do personagem
		StartScene.SetActive (false);

	}

	void update(){
		if (Input.GetKeyDown(KeyCode.Space))
			print("space key was pressed");
		else if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey("enter")) 
			print("enter key was pressed");

	}
}
