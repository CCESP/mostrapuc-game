using UnityEngine;
using System.Collections;

public class TriggerDialog : MonoBehaviour {
	
    public string nomeStand;

	void OnTriggerEnter2D(Collider2D other) {
		StandsControl controller = transform.parent.parent.GetComponent<StandsControl>();
        controller.ExibeStand(nomeStand);
        GameObject personagem = other.gameObject;
        personagem.GetComponent<Animator> ().enabled = false;   //Parar a animação
        personagem.GetComponent<PlayerMovement>().speed = 0f;	//Para a movimentação do personagem
		controller.nextScene.SetActive(true);
	}
    
	void OnTriggerStay2D(){
		//Debug.Log ("Caio Rushado! 2");
	}

	void OnTriggerExit2D(){
		//Debug.Log ("Caio Rushado! 4");
	}
		
}
