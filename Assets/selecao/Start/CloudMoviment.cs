using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoviment : MonoBehaviour {

    private float CLOUD_SPEED = 0.013f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.left * CLOUD_SPEED);
        if(transform.localPosition.x < -750f)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
