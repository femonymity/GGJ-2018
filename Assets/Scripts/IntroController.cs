using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class IntroController : MonoBehaviour {

	public Text text;
	public List<Text> allText;

	void Start() {
		StartCoroutine ("showText", allText);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("jump")) {
			text.text = "Loading...";
			StartCoroutine ("loadLevelOne");
		}
	}

	IEnumerator showText(List<Text> introText) {
		Text currentText = introText[0];
		introText.RemoveAt (0);

		for (float f = 0f; f<=1; f += 0.02f) {
			Color c = currentText.color;
			c.a = f;
			currentText.color = c;
			yield return null;
		}
			
		yield return new WaitForSeconds (1.5f);

		for (float f = 1f; f>=0; f-= 0.02f) {
			Color c = currentText.color;
			c.a = f;
			currentText.color = c;
			yield return null;
		}

		yield return new WaitForSeconds (1.5f);

		if (introText.Count > 0) {
			StartCoroutine ("showText", introText);
		} else {
			text.text = "Loading...";
			StartCoroutine ("loadLevelOne");
		}
	}

	IEnumerator loadLevelOne() {
		AsyncOperation aSyncLoad = SceneManager.LoadSceneAsync ("Main");
		while(!aSyncLoad.isDone) {
			yield return null;
		}
	}
}
