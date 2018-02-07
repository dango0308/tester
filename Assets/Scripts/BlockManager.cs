using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

	public GameObject gameManager;

	public AudioClip bombSE;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = this.gameManager.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Fire") {
			Destroy (this.gameObject);
			audioSource.PlayOneShot (bombSE);
		}
	}
}
