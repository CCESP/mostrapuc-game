﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObstacle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnBecameInvisible()
    {
        if(transform.parent != null) {
            GameObject.Destroy(transform.parent.gameObject);
        } else
        {
            GameObject.Destroy(this);
        }
    }
}
