using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AviaoPrefabUpdate : MonoBehaviour {

    private const float vooAviaoDiff = 0.02f;
    private const float velocidadeAviaoDiff = 0.0075f;

    private GameSceneController gsc;

    // Use this for initialization
    void Start () {
        gsc = this.transform.parent.parent.gameObject.GetComponent<GameSceneController>();
    }
	
	// Update is called once per frame
	void Update () {
        if(gsc.IsRunning()) {
            transform.position = new Vector3(transform.position.x + velocidadeAviaoDiff, transform.position.y + Mathf.Sin(Time.time * 2) * vooAviaoDiff, transform.position.z);
        }
    }
}
