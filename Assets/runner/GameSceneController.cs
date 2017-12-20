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
	public GameObject safeBlock;
    public Text countdownText;
    public Text countdownLabelText;

    private float  gamePointer;
    private const float obstacleDistanceSpawn = 7.5f;
	private const float safeSpawningArea = 25;
	private const float safeDestroyArea = 30;
    private const float playerOffsetX = 4f;
    private const int maxCountToBegin = 3;
    
    private float lastScore = 0f;
    private float deltaDistance = 0f;

    private bool running = false;

	//Manter referencia dos blocos gerados para destrui-los
	private LinkedList<GameObject> generatedBlocks = new LinkedList<GameObject>();

    public bool IsRunning ()
    {
        return running;
    }

	void Start () {
        running = false;
		generatedBlocks.AddLast(safeBlock);
        countdownLabelText.text = "Iniciando corrida em:";
        CountdownText(true);
    }

    public void GameOver(bool win)
    {
        running = false;
        countdownLabelText.text = "Você perdeu! Retornando a tela inicial em:";
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
            float score;

            if (player.transform.position.x > 0)
				score = player.transform.position.x;
			else
				score = 0f;

            // verificando se spawna obstáculo
            deltaDistance += (score - lastScore);

            lastScore = score;

            if (deltaDistance > obstacleDistanceSpawn)
            {
                deltaDistance = 0f;
                int obstacleIdx = Random.Range(0, obstaclePrefab.Length);
                GameObject obstacleObject = Instantiate(obstaclePrefab[obstacleIdx]);
                obstacleObject.transform.SetParent(this.transform);
                obstacleObject.transform.position = new Vector3(gameCamera.transform.position.x + obstacleObject.transform.position.x, obstacleObject.transform.position.y, 11);
            }

            scoreText.text = "Score: " + Mathf.Floor (score);

			//Destruindo primeiro bloco da lista caso esteja longe
			GameObject firstBlock = generatedBlocks.First.Value;
			if (firstBlock.transform.position.x < player.transform.position.x - safeDestroyArea) {
				generatedBlocks.RemoveFirst();
				GameObject.Destroy(firstBlock);
			}

		} /* fim if player != null */
	}
    
}
