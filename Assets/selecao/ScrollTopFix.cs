using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Script to set scrollbar at top when loading new gameobject
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
		yield return null; // Waiting just one frame is probably good enough, yield return null does that
		Scrollbar bar = GetComponentInChildren<Scrollbar>();
		bar.value = 1;
	}

}
