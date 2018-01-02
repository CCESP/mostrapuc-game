using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animatefloor : MonoBehaviour {

    public Material mat;
    private float SPEED_X = 0.00125f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 v = mat.mainTextureOffset;
        v.Set(v.x + SPEED_X, v.y);
        mat.mainTextureOffset = v;
	}
}
