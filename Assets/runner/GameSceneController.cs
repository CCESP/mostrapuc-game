using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameSceneController : MonoBehaviour {

	public Player player;
	public Camera gameCamera;
	public GameObject[] blockPrefabs;
	public Text scoreText;
	public GameObject safeBlock;

	private float  gamePointer;
	private const float safeSpawningArea = 25;
	private const float safeDestroyArea = 30;

	//Manter referencia dos blocos gerados para destrui-los
	private LinkedList<GameObject> generatedBlocks = new LinkedList<GameObject>();

	void Start () {
		generatedBlocks.AddLast(safeBlock);
	}

	void Update ()
	{
		if (player != null) {
			//Fazendo Camera Seguir o Player
			gameCamera.transform.position = new Vector2 (player.transform.position.x, gameCamera.transform.position.y);

			//Gerando os blocos
			while (player != null && gamePointer < player.transform.position.x + safeSpawningArea) {

				int blockIndex = Random.Range (0, blockPrefabs.Length);
				GameObject blockObject = Instantiate (blockPrefabs [blockIndex]);
				blockObject.transform.SetParent (this.transform);
				Block block = blockObject.GetComponent<Block> ();
				blockObject.transform.position = new Vector3 (gamePointer + block.size / 2, -3.21f, 11);
				gamePointer += block.size;
				Debug.Log(block.size);
				generatedBlocks.AddLast (blockObject);

			}

			//Settando Score
			float score;

			if (player.transform.position.x > 0)
				score = player.transform.position.x;
			else
				score = 0f;

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
