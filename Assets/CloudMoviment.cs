using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoviment : MonoBehaviour {

	public float offscreen = 380.0f;
	public float spawn = -120.0f;
	public float speed = 0.01f;
	public float lowest = 195.0f;
	public float highest = 198.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//amount to move cloud
		//move enemy
		transform.Translate(Vector3.right * speed);
		//respawn with random Y
		/*if (transform.position.x <= offscreen) {

			Vector3 temp = transform.position;
			temp.x = spawn;
			temp.y = Random.Range (highest , lowest);
			transform.position = temp;
		}*/
	}
}
