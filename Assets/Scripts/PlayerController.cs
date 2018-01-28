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
	public bool godMode;

	private float fallSpeed = -12.0f;
	private float jumpSpeed = 12.0f;

	public Vector2 startLocation;
	public float inputDelay = 2.5f;
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
				rb.velocity = new Vector2 (rb.velocity.x, fallSpeed);
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
		if (godMode) {
			GameObject noteParent = GameObject.FindGameObjectWithTag ("NoteParent");
			foreach (Transform child in noteParent.transform) {
				NoteMover note = child.GetComponent<NoteMover> ();
				if (note.isInZone ()) {
					note.gameObject.GetComponent<ParticleSystem> ().Play();
					note.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
					correctInputs.Enqueue(new PlayerInput(note.inputName));
				}
			}
		} else {
			string inputName = "";
			bool success = false;
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

			if (inputName != string.Empty) {
				success = destroyCorrectNote (inputName);
				if (success) {
					Debug.Log ("hit");
					correctInputs.Enqueue(new PlayerInput(inputName));
				} else {
					Debug.Log ("miss");
					Invoke (inputName, inputDelay);
				}
			}
		}
	}

	private bool destroyCorrectNote(string inputName) {
		bool success = false;
		GameObject noteParent = GameObject.FindGameObjectWithTag ("NoteParent");
		foreach (Transform child in noteParent.transform) {
			NoteMover note = child.GetComponent<NoteMover> ();
			if (note.isInZone () && note.inputName == inputName) {
				note.gameObject.GetComponent<ParticleSystem> ().Play();
				note.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				success = true;
			}
		}
		return success;
	}

	private void jump() {
		if (!falling && !jumping && !hanging) {
			anim.SetTrigger ("Jump");

			jumping = true;
			jumpTime = 0.25f;
			airTime = 0.1f;
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}
	}

	private void duck() {
		if (!falling && !jumping && !hanging) {
			anim.SetTrigger ("Slide");
		}
	}

	private void longjump() {
		if (!falling && !jumping && !hanging) {
			anim.SetTrigger ("Jump");

			jumping = true;
			jumpTime = 0.2f;
			airTime = 0.7f;
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}
	}

	private void highjump() {
		if (!falling && !jumping && !hanging) {
			anim.SetTrigger ("Jump");

			jumping = true;
			jumpTime = 0.35f;
			airTime = 0.05f;
			rb.velocity = new Vector2 (rb.velocity.x, jumpSpeed);
		}
	}

	public void spawnPlayer() {
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
