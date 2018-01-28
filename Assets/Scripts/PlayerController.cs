using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Queue<PlayerInput> correctInputs;
	private PlayerInput currentInput;
	private GameController gameCon;
	private Animator anim;
	private Animator midiAnim;
	private float timeToBeat;
	private float jumpTime;
	private float airTime;

	public bool hanging = false;
	public bool jumping = false;
	public bool falling = false;
	public bool sliding = false;

	public Vector2 startLocation;
	public float inputDelay = 6 / 7;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		correctInputs = new Queue<PlayerInput> ();
		gameCon = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
		anim = GetComponent<Animator> ();
		midiAnim = GameObject.FindGameObjectWithTag("M1D1").GetComponent<Animator>();

		startLocation = new Vector2 (transform.position.x, transform.position.y);
	}

	// Update is called once per frame
	void Update () {
		getPlayerInput ();

		if (jumping) {
			jumpTime -= Time.deltaTime;
			if (jumpTime <= 0.0f) {
				jumping = false;
				hanging = true;
				rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
			}
		} else if (hanging) {
			airTime -= Time.deltaTime;
			if (airTime <= 0.0f) {
				anim.SetTrigger ("Falling");
				hanging = false;
				falling = true;
				rb.velocity = new Vector2 (rb.velocity.x, -8.0f);
			}
		}
	}

	void OnCollisionEnter2D (Collision2D other) {
		if ((other.gameObject.tag == "Terrain") && falling) {
			rb.velocity = new Vector2 (rb.velocity.x, 0.0f);
			anim.SetTrigger ("Landing");
			falling = false;
		} 
	}

	void OnTriggerEnter2D (Collider2D other) {
		if (other.gameObject.tag == "Obstacle") {
			killPlayer ();
		} else if (other.gameObject.tag == "ActionTrigger") {
			if (correctInputs.Count > 0) {
				PlayerInput nextInput = correctInputs.Dequeue ();
				Invoke (nextInput.inputName , 0.0f);
			}
		}
	}

	private void getPlayerInput() {
		string inputName = "";
		if (Input.GetButtonDown ("jump")) {
			inputName = "jump";
			midiAnim.SetTrigger ("Jump");
		} else if (Input.GetButtonDown ("duck")) {
			inputName = "duck";
			midiAnim.SetTrigger ("Duck");
		} else if (Input.GetButtonDown ("longjump")) {
			inputName = "longjump";
			midiAnim.SetTrigger ("LongJump");
		} else if (Input.GetButtonDown ("highjump")) {
			inputName = "highjump";
			midiAnim.SetTrigger ("HighJump");
		}

		if ((inputName != string.Empty)) {
			destroyCorrectNote (inputName);
			Invoke (inputName, inputDelay);
		}
	}

	private void destroyCorrectNote(string inputName) {
		GameObject noteParent = GameObject.FindGameObjectWithTag ("NoteParent");
		foreach (Transform child in noteParent.transform) {
			NoteMover note = child.GetComponent<NoteMover> ();
			if (note.isInZone () && note.inputName == inputName) {
				note.gameObject.GetComponent<ParticleSystem> ().Play();
				note.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}

	private void jump() {
		anim.SetTrigger ("Jump");

		jumping = true;
		jumpTime = 0.25f;
		airTime = 0.1f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	private void duck() {
		anim.SetTrigger ("Slide");
	}

	private void longjump() {
		//TODO play animation
		anim.SetTrigger ("Jump");

		jumping = true;
		jumpTime = 0.2f;
		airTime = 0.7f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	private void highjump() {
		//TODO play animation
		anim.SetTrigger ("Jump");

		jumping = true;
		jumpTime = 0.35f;
		airTime = 0.05f;
		rb.velocity = new Vector2 (rb.velocity.x, 12.0f);
	}

	public void spawnPlayer() {
		//TODO
		rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
		transform.position = startLocation;
		enablePlayer();
		GetComponent<SpriteRenderer> ().enabled = true;
		Debug.Log ("player spawned");
	}

	private void killPlayer() {
		rb.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
		MusicController musicCon = GameObject.FindGameObjectWithTag ("MusicController").GetComponent<MusicController> ();
		gameCon.stopAllNotes ();
		musicCon.pauseMusic();
		musicCon.playDeathSound ();

		anim.SetTrigger ("Death");

		correctInputs.Clear ();
		CancelInvoke ();
	}

	public void endOfPlayerDeath() {
		disablePlayer ();
		gameCon.resetLevel ();
	}

	public void disablePlayer() {
		gameObject.SetActive (false);
	}

	public void enablePlayer() {
		gameObject.SetActive (true);
	}
}
