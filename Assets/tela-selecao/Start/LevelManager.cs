using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void loadLevel(string newLevel){
		Debug.Log ("Level load requested for: "+newLevel);
		Application.LoadLevel (newLevel);
	}
	public void quitRequest(){
		Debug.Log ("Quit requested.");
		Application.Quit();
	}
}


