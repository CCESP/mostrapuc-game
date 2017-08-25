using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {

	public void cancel(){
		Debug.Log ("Cancel");
        retomarMovimentoPersonagem();
    }
	public void ok(){
		Debug.Log ("Ok.");
        retomarMovimentoPersonagem();
	}

    private void retomarMovimentoPersonagem ()
    {
        GameObject teste = GameObject.Find("personagemJogador");
        teste.GetComponent<Animator>().enabled = true;  // Retoma a animação
        teste.GetComponent<PlayerMovement>().speed = 4.0f;  // Permite a movimentação do personagem
        GameObject StartScene = transform.parent.GetComponent<StartScene>().nextScene;
        StartScene.SetActive(false);
    }    
}
