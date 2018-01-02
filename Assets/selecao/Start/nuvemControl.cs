using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nuvemControl : MonoBehaviour {

    private float POS_X_SPAWN = 540f;
    private float POS_Y_SPAWN = 90f;
    private float POS_Y_RNG = 50f;
    private float MIN_SCALE = 30f;
    private float MAX_SCALE = 80f;
    private float POS_Z = 0;
    public GameObject nuvemPrefab;

	// Use this for initialization
	void Start () {
        InvokeRepeating("spawnNuvem", 1.0f, 7.5f);
	}

	private void spawnNuvem ()
    {
        float y = POS_Y_SPAWN - POS_Y_RNG + Random.Range(0, POS_Y_RNG * 2);
        GameObject c = Instantiate(nuvemPrefab);
        c.transform.SetParent(this.transform);
        c.transform.localPosition = new Vector3(POS_X_SPAWN, y, POS_Z);
        float scale = Random.Range(MIN_SCALE, MAX_SCALE);
        c.transform.localScale = new Vector3(scale, scale, 1);
    }
	
    // Update is called once per frame
	void Update () {
		
	}
}
