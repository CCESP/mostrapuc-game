using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour {

	private Vector3 projectileOverheadPosition = new Vector3(-9f, -6.5f, 0f);
	private Vector3 projectileGroundPosition = new Vector3(-10f, -8f, 0f);
	private float moveTimeGround = 1f;
	private float moveTimeHead = 2f;

	private float projectileSpawnXDiff = -1.0f;

	private float moveTime;
	private Vector3 targetPosition;
	private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
		this.transform.position = GameObject.FindGameObjectWithTag("RunnerEnemy").transform.position;
		this.transform.position += new Vector3(projectileSpawnXDiff, 0, 0);

		Vector3 added;

		if (Random.Range (0, 10) > 5) {
			moveTime = moveTimeGround;
			added = projectileGroundPosition;
		} else {
			moveTime = moveTimeHead;
			added = projectileOverheadPosition;
		}

		targetPosition = transform.localPosition + added;
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, targetPosition, ref velocity, moveTime);
	}
}