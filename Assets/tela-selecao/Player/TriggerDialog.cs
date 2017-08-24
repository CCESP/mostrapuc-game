using UnityEngine;
using System.Collections;

public class TriggerDialog : MonoBehaviour {
	
	private GameObject StartScene;
	private Animator Anim;

	void OnTriggerEnter2D(Collider2D other) {
		StartScene = transform.parent.parent.GetComponent<StartScene>().nextScene;
		GameObject teste = other.gameObject;
		teste.GetComponent<Animator> ().enabled = false;	//Parar a animação
		teste.GetComponent<PlayerMovement>().speed = 0f;	//Para a movimentação do personagem
		StartScene.SetActive (true);
	}

	void OnTriggerStay2D(){
		//Debug.Log ("Caio Rushado! 2");
	}

	void OnTriggerExit2D(){
		//Debug.Log ("Caio Rushado! 4");
	}
		
}
