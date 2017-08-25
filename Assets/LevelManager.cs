using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public void loadNextLevel() {
        int idx = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(idx);
	}

	public void quitRequest(){
		Debug.Log ("Quit requested.");
		Application.Quit();
	}
}


