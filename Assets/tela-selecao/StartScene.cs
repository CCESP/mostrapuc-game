using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
	
	public GameObject nextScene;
	
	void Start(){
		nextScene = transform.Find("GameObject").gameObject;
		nextScene.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
		if (nextScene.activeInHierarchy) {	//nextScene ativo
			if (Input.GetKeyDown (KeyCode.Space))
				cancel ();
			else if (Input.GetKeyDown (KeyCode.KeypadEnter))
				ok ();
		}
	}
		
	void cancel(){
		
		Debug.Log ("Cancel");
		GameObject teste = GameObject.Find("newPlayer");
		teste.GetComponent<Animator> ().enabled = true;	//Parar a animação
		teste.GetComponent<PlayerMovement>().speed = 4.0f;	//Para a movimentação do personagem
		nextScene.SetActive (false);

	}

	void ok(){
		
		Debug.Log ("Ok.");
		GameObject teste = GameObject.Find("newPlayer");
		teste.GetComponent<Animator> ().enabled = true;	//Parar a animação
		teste.GetComponent<PlayerMovement>().speed = 4.0f;	//Para a movimentação do personagem
		nextScene.SetActive (false);

	}

}
