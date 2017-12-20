using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoviment : MonoBehaviour {

	public float speed;
	public bool direction;
	public Camera camera = null;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		//amount to move cloud
		//move enemy
		if(direction)
			transform.Translate(Vector3.right * speed);
		else
			transform.Translate(Vector3.left * speed);
		
		Vector3 screenPos;

		screenPos = Camera.main.WorldToScreenPoint(transform.position);

		if (screenPos.x < 0)
			direction = !direction;
		else if(screenPos.x > Screen.width)
			direction = !direction;
	}
}
