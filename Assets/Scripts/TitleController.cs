using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class TitleController : MonoBehaviour {

	public Text text;
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("jump")) {
			text.text = "Loading...";
			StartCoroutine ("loadIntro");
		}
	}

	IEnumerator loadIntro() {
		AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync ("Intro");
		while(!aSyncLoad.isDone) {
			yield return null;
		}
	}
}
