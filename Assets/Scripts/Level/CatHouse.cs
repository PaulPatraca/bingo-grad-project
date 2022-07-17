using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatHouse : MonoBehaviour
{
    public Sprite filledHouse;
    private Collider2D homeFree;
	private SpriteRenderer spriteRen;
	private void Start() {
		homeFree = GetComponent<Collider2D>();
		spriteRen = GetComponent<SpriteRenderer>();
	}
	public void toggleHouse() {
		homeFree.enabled = true;
		spriteRen.sprite = filledHouse;
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject coll = other.transform.gameObject;
		if (coll.CompareTag("Player")) {
			Rigidbody2D body = other.GetComponent<Rigidbody2D>();
			coll.GetComponent<PlayerMain>().WinGame();
			coll.GetComponent<Movement>().canMove = false;
		}
	}
}
