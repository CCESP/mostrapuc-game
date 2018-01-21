using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    private Text countDown;
    private float start;
    private float COUNTDOWN_TIME = 9.9f;
    public LevelManager levelManager;
    
	// Use this for initialization
	void Start () {
        countDown = transform.Find("title1").GetComponent<Text>();
        start = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        float diff = Time.time - start;
        if(diff >= COUNTDOWN_TIME)
        {
            levelManager.loadNextLevel();
        }
        countDown.text = "Obrigado por jogar! Retornando a tela inicial em " + ((int) (COUNTDOWN_TIME - diff) + 1) + " segundos...";
	}

}
