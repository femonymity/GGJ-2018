using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class CreditsController : MonoBehaviour {

	public Text thanksText;
	public List<Text> credits;

	// Use this for initialization
	void Start () {
		StartCoroutine ("showText");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator showText() {
		Text currentText = thanksText;

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

		yield return new WaitForSeconds (1.0f);

		showCredits (credits);
	}

	void showCredits(List<Text> creditList) {

		foreach(Text credit in creditList) {
			StartCoroutine ("showCredit", credit);
		}
	}

	IEnumerator showCredit(Text credit) {
		Text currentText = credit;

		for (float f = 0f; f<=1; f += 0.02f) {
			Color c = currentText.color;
			c.a = f;
			currentText.color = c;
			yield return null;
		}
	}
}
