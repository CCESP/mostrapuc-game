using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text encontrados = (Text) transform.Find("title2").GetComponent<Text>();
        encontrados.text = "Informações Recuperadas: " + ScoreManager.GOAL_LAST_SCORE;
        Text itens = (Text)transform.Find("body").GetComponent<Text>();
        string buf = "";
        int count = ScoreManager.GOAL_LAST_SCORE - 1;
        for (int i = ScoreManager.GOAL_STR_LIST.Length - 1; i >= 0; i--)
        {
            string s = "??????????";
            if(count >= 0 && (count == i || Random.Range(0f, 1f) >= 0.5))
            {
                s = ScoreManager.GOAL_STR_LIST[i];
                count--;
            }
            buf += (ScoreManager.GOAL_STR_LIST.Length - i) + " - " + s + "\n";
        }
        itens.text = buf;
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
