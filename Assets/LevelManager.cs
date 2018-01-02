using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public const string START_SCREEN = "p1";
    public const string INTRO_SELECAO_SCREEN = "IntroSelecao";
    public const string STANDS_SCREEN = "p2";
    public const string INTRO_RUNNER_SCREEN = "IntroRunner";
    public const string RUNNER_SCREEN = "Runner";
    
    public Canvas canvas;

    public void Start()
    {
        exibirTelaInicial();
    }

    private string getNextLevel()
    {
        string levelName = SceneManager.GetActiveScene().name;
        string ret = "";

        switch (levelName)
        {
            case START_SCREEN:
                ret = STANDS_SCREEN;
                break;
            case STANDS_SCREEN:
                ret = RUNNER_SCREEN;
                break;
            case RUNNER_SCREEN:
                ret = START_SCREEN;
                break;
        }

        return ret;
    }

    public void loadNextLevel() {
        string nextLevel = getNextLevel();
        SceneManager.LoadScene(nextLevel);
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


