using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapLeftManager : MonoBehaviour {

	public GameObject needle; //針トラップ
	private bool trap = false; //trapAleaに反応したかどうか
	public float needleSpeed;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(trap){
			needle.transform.position -= new Vector3 (needleSpeed, 0.0f, 0.0f); //針をy方向い動かす
		}

		if (needle.transform.position.x < -10) {
			Destroy (needle);
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D (Collider2D col){
		if (col.gameObject.tag == "Player") {
			trap = true; //trapAleaに反応
		}
	}
}