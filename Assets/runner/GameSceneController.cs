using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameSceneController : MonoBehaviour {

	public Player player;
    public Enemy enemy;
    public Camera gameCamera;
	public GameObject floorPrefab;
    public GameObject[] obstaclePrefab;
    public Text scoreText;
	public Text goalText;
	public GameObject safeBlock;
    public Text countdownText;
    public Text countdownLabelText;

	private int MAX_GOAL = ScoreManager.GOAL_STR_LIST.Length;
    private float  gamePointer;
    private const float obstacleDistanceSpawn = 7.5f;
	private const float safeSpawningArea = 25;
	private const float safeDestroyArea = 30;
    private const float playerOffsetX = 4f;
    private const int maxCountToBegin = 3;

	private int currentGoal = 0;
	private int score = 0;
	private int extraScore = 0;
    private float lastDist = 0f;
    private float deltaDistance = 0f;

    private bool running = false;

	//Manter referencia dos blocos gerados para destrui-los
	private LinkedList<GameObject> generatedBlocks = new LinkedList<GameObject>();

	public void AddGoalScore(int n) {
		ScoreManager.GOAL_LAST_SCORE = currentGoal = Mathf.Min(MAX_GOAL, currentGoal + 1);
		extraScore += Mathf.Abs(n);
	}

    public bool IsRunning ()
    {
        return running;
    }

	void Start () {
        ScoreManager.GOAL_LAST_SCORE = currentGoal;
        running = false;
		generatedBlocks.AddLast(safeBlock);
        countdownLabelText.text = "Iniciando corrida em:";
        CountdownText(true);
    }

    public void GameOver(bool win)
    {
        running = false;
        countdownLabelText.text = "Você foi atingido! Exibindo tela de resultados em:";
        CountdownText(false);
    }

    void CountdownText(bool newGame)
    {
        countdownText.text = maxCountToBegin + "";
        countdownText.enabled = true;
        countdownLabelText.enabled = true;
        for (int i = maxCountToBegin; i >= 0; i--)
        {
            StartCoroutine(DisplayCountNumber(i, newGame));
        }
    }

    private IEnumerator DisplayCountNumber(int number, bool newGame)
    {
        yield return new WaitForSeconds(maxCountToBegin - number);

        if (number == 0) {

            if(newGame) {
                player.EnablePlayer();
                countdownText.enabled = false;
                countdownLabelText.enabled = false;
                scoreText.enabled = true;
                running = true;
                enemy.Spawn();
            }
            else {
                GetComponent<LevelManager>().loadNextLevel();
            }

        } else {
            countdownText.text = number + "";
        }
    }

	void Update ()
	{
		if (player != null) {
			//Fazendo Camera Seguir o Player
			gameCamera.transform.position = new Vector2 (player.transform.position.x + playerOffsetX, gameCamera.transform.position.y);

			//Gerando o chão
			if (player != null && gamePointer < player.transform.position.x + safeSpawningArea) {
				GameObject blockObject = Instantiate (floorPrefab);
                blockObject.transform.SetParent (this.transform);
                blockObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                Block block = blockObject.GetComponent<Block> ();
				blockObject.transform.position = new Vector3 (gamePointer + block.size / 2, -3.21f, 11);
				gamePointer += block.size;
				generatedBlocks.AddLast (blockObject);
			}

            //Settando Score
			float dist;

            if (player.transform.position.x > 0)
				dist = player.transform.position.x;
			else
				dist = 0f;

            // verificando se spawna obstáculo
			deltaDistance += (dist - lastDist);

            lastDist = dist;
			score = (int) dist;

            if (deltaDistance > obstacleDistanceSpawn)
            {
                deltaDistance = 0f;
                int obstacleIdx = Random.Range(0, obstaclePrefab.Length);
                GameObject obstacleObject = Instantiate(obstaclePrefab[obstacleIdx]);
                obstacleObject.transform.SetParent(this.transform);
                obstacleObject.transform.position = new Vector3(gameCamera.transform.position.x + obstacleObject.transform.position.x, obstacleObject.transform.position.y, 11);
            }

            scoreText.text = "Pontos: " + Mathf.Floor (score + extraScore);
			goalText.text = "Documento: " + currentGoal + "/" + MAX_GOAL;

			//Destruindo primeiro bloco da lista caso esteja longe
			GameObject firstBlock = generatedBlocks.First.Value;
			if (firstBlock.transform.position.x < player.transform.position.x - safeDestroyArea) {
				generatedBlocks.RemoveFirst();
				GameObject.Destroy(firstBlock);
			}

		} /* fim if player != null */
	}
    
}
