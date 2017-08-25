﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class GameSceneController : MonoBehaviour {

	public Player player;
	public Camera gameCamera;
	public GameObject floorPrefab;
    public GameObject[] obstaclePrefab;
    public Text scoreText;
	public GameObject safeBlock;

	private float  gamePointer;
    private const float obstacleDistanceSpawn = 7.5f;
	private const float safeSpawningArea = 25;
	private const float safeDestroyArea = 30;
    private const float playerOffsetX = 7.5f;

    private float lastScore = 0f;
    private float deltaDistance = 0f;
    
	//Manter referencia dos blocos gerados para destrui-los
	private LinkedList<GameObject> generatedBlocks = new LinkedList<GameObject>();

	void Start () {
		generatedBlocks.AddLast(safeBlock);
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