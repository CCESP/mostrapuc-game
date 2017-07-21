using UnityEngine;
using System.Collections;

public class StartScene : MonoBehaviour {
	
	public GameObject nextScene;
	
	void Start(){
		nextScene = transform.Find("GameObject").gameObject;
		nextScene.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
