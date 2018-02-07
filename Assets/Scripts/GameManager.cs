using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject textGameOver;
	public GameObject textGameClear;
	public GameObject buttons;
	public GameObject stageSelectButton;
	public GameObject retryButton;

	public int stageNo;

	public AudioClip gameoverSE;
	public AudioClip gameclearSE;
	public AudioClip pushButtonSE;
	private AudioSource audioSource;

	public enum GAME_MODE // ゲーム状態定義
	{
		PLAY,			  // プレイ中
		CLEAR,			  // クリア
	};

	public GAME_MODE gameMode = GAME_MODE.PLAY; // ゲーム状態

	// Use this for initialization
	void Start () {
		audioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void GameOver(){
		if (gameMode == GAME_MODE.CLEAR) {
			return;
		}
		textGameOver.SetActive (true);
		buttons.SetActive (false);
		stageSelectButton.SetActive (true);
		retryButton.SetActive (true);
		audioSource.PlayOneShot (gameoverSE);
	}

	public void GameClear(){
		gameMode = GAME_MODE.CLEAR;
		textGameClear.SetActive (true);
		buttons.SetActive (false);
		stageSelectButton.SetActive (true);
		audioSource.PlayOneShot (gameclearSE);

		if (PlayerPrefs.GetInt ("CLEAR", 0) < stageNo) {
			PlayerPrefs.SetInt ("CLEAR", stageNo);
		}
	}

	public void StageSelectButton(){
		SceneManager.LoadScene ("StageSelectScene");
		audioSource.PlayOneShot(pushButtonSE);
	}

	public void RetryButton(int stageNo){
		SceneManager.LoadScene ("GameScene" + stageNo);	//ゲームシーンへ
		audioSource.PlayOneShot(pushButtonSE);
	}
}
