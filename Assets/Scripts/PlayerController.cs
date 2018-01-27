using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

	public const string INPUT_JUMP = "jump";

	private List<PlayerInput> inputs;
	private int maxInputs { get; set; }

	// Use this for initialization
	void Start () {
		inputs = new List<PlayerInput> ();

	}
	
	// Update is called once per frame
	void Update () {
		getPlayerInput ();
	}

	private void getPlayerInput() {

		if ()
		
		if (Input.GetButtonDown ("jump")) {
			inputs.Add (new PlayerInput ("jump"));
		} else if (Input.GetButtonDown ("duck")) {
			inputs.Add (new PlayerInput ("duck"));
		} else if (Input.GetButtonDown ("longjump")) {
			inputs.Add (new PlayerInput ("longjump"));
		} else if (Input.GetButtonDown ("highjump")) {
			inputs.Add (new PlayerInput ("highjump"));
		}
	}
}
