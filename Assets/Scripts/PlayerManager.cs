using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private Rigidbody2D rbody;

	public GameObject gameManager;

	private const float MOVE_SPEED = 4; //移動速度固定値
	private float moveSpeed; //移動速度

	private const int MAX_JUMP_COUNT = 2; //ジャンプ回数
	private float jumpForce = 500.0f; //ジャンプ力

	private int jumpCount = 0; //現在のジャンプ回数
	private bool isJump = false; //ジャンプしたか否か

	private bool usingButtons = false;　//ボタンを押しているか否か

	public AudioClip jumpSE;
	private AudioSource audioSource;

	public enum MOVE_DIR //移動方向定義
	{
		STOP,
		LEFT,
		RIGHT,
	};

	private MOVE_DIR moveDirection = MOVE_DIR.STOP; //移動方向

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody2D> ();
		audioSource = gameManager.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!usingButtons) { //キーボードでの移動
			float x = Input.GetAxisRaw ("Horizontal");
			if (x == 0) {
				moveDirection = MOVE_DIR.STOP;
			} else {
				if (x < 0) {
					moveDirection = MOVE_DIR.LEFT;
				} else {
					moveDirection = MOVE_DIR.RIGHT;
				}
			}
		}

		if (jumpCount < MAX_JUMP_COUNT && Input.GetKeyDown ("c")) {
			isJump = true;
			audioSource.PlayOneShot (jumpSE);
		}
	}

	void FixedUpdate(){ //プレイヤー移動
		switch (moveDirection) {
		case MOVE_DIR.STOP:
			moveSpeed = 0;
			break;
		case MOVE_DIR.LEFT:
			moveSpeed = MOVE_SPEED * -1;
			break;
		case MOVE_DIR.RIGHT:
			moveSpeed = MOVE_SPEED;
			break;
		}
		rbody.velocity = new Vector2 (moveSpeed, rbody.velocity.y);

		if (isJump) {
			rbody.velocity = Vector2.zero; //速度をクリアして2回目のジャンプも1回目と同じ挙動にする

			rbody.AddForce (Vector2.up * jumpForce); //ジャンプさせる

			jumpCount++; //ジャンプ回数をカウント

			isJump = false; //ジャンプを許可する
		}
	}

	public void PushLeftButton(){ //左移動ボタン
		moveDirection = MOVE_DIR.LEFT;
		usingButtons = true;
	}

	public void PushRightButton(){ //右移動ボタン
		moveDirection = MOVE_DIR.RIGHT;
		usingButtons = true;
	}

	public void ReleaseMoveButton(){ //ボタンを離したとき止まる
		moveDirection = MOVE_DIR.STOP;
		usingButtons = false;
	}

	public void PushJumpButton(){
		if (jumpCount < MAX_JUMP_COUNT) { //ジャンプボタン
			isJump = true;
		}
	}

	void OnCollisionEnter2D(Collision2D col){
		if (gameManager.GetComponent<GameManager> ().gameMode != GameManager.GAME_MODE.PLAY) {
			return;
		}

		if (col.gameObject.tag == "Block") {
			jumpCount = 0;
		}

		if (col.gameObject.tag == "OutAlea") {
			Destroy (this.gameObject);
			gameManager.GetComponent<GameManager> ().GameOver ();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag == "Goal"){
			gameManager.GetComponent<GameManager> ().GameClear ();
		}
	}
}