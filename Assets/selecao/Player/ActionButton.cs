using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionButton : MonoBehaviour {

    private LevelManager manager;

    public void Start()
    {
        manager = transform.GetComponent<LevelManager>();
    }

	public void cancel(){
		Debug.Log ("Cancel");
        retomarMovimentoPersonagem();
    }
	public void ok(){
		Debug.Log ("Ok.");
        manager.loadNextLevel();
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
