﻿using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public const string START_SCREEN = "p1";
    public const string INTRO_SELECAO_SCREEN = "IntroSelecao";
    public const string STANDS_SCREEN = "p2";
    public const string INTRO_RUNNER_SCREEN = "IntroRunner";
    public const string RUNNER_SCREEN = "Runner";
    public const string RESULT_SCREEN = "RunnerResultScreen";
    public const string GAME_OVER_SCREEN = "GameOver";

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
				ret = INTRO_SELECAO_SCREEN;
                break;
			case INTRO_SELECAO_SCREEN:
				ret = STANDS_SCREEN;
				break;
            case STANDS_SCREEN:
				ret = INTRO_RUNNER_SCREEN;
                break;
			case INTRO_RUNNER_SCREEN:
				ret = RUNNER_SCREEN;
				break;
            case RUNNER_SCREEN:
                ret = RESULT_SCREEN;
                break;
            case RESULT_SCREEN:
                ret = GAME_OVER_SCREEN;
                break;
            case GAME_OVER_SCREEN:
                ret = START_SCREEN;
                break;
        }

        return ret;
    }

    public void loadNextLevel() {
        string nextLevel = getNextLevel();
        SceneManager.LoadScene(nextLevel);
	}

	public void exibirCreditos() {
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


