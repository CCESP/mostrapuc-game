using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public Canvas canvas;

    public void Start()
    {
        exibirTelaInicial();
    }

	public void loadNextLevel() {
        int idx = (SceneManager.GetActiveScene().buildIndex + 1) % SceneManager.sceneCountInBuildSettings;
        SceneManager.LoadScene(idx);
	}

	public void exibirCreditos(){
        canvas.transform.Find("credits").gameObject.SetActive(true);
        canvas.transform.Find("startScreen").gameObject.SetActive(false);
    }

    public void exibirTelaInicial()
    {
        if(canvas != null) {
            canvas.transform.Find("credits").gameObject.SetActive(false);
            canvas.transform.Find("startScreen").gameObject.SetActive(true);
        }
    }
}


