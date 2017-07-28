using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollTopFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnEnable() {
		StartCoroutine(SetScrollTop());
	}

	public IEnumerator SetScrollTop () {
		Debug.Log("Now its called");
		yield return null; // Waiting just one frame is probably good enough, yield return null does that
		Scrollbar bar = GetComponentInChildren<Scrollbar>();
		bar.value = 1;
		Debug.Log("Now its setted");
	}

}
