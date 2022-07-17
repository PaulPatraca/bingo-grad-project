using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class GameEnder : MonoBehaviour {
	protected SpriteRenderer sprite;
	protected Transform pos;
	protected Vector3 origPos;
	private AudioHandler audHand;
	public Sprite winScreen;
	protected void Start() {
		if (GetComponent<SpriteRenderer>())
			sprite = GetComponent<SpriteRenderer>();
		if (GetComponent<Transform>())
			pos = GetComponent<Transform>();
		origPos = pos.position;
		audHand = GetComponent<AudioHandler>();
	}
	public void GoodNight() {
		pos.DOLocalMove(new Vector3(0, 0f, -9f), 1.5f);
		sprite.DOColor(new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1.0f), 1.5f);
		if (sprite.sprite != winScreen) {
			audHand.PlayClip("lose");
			//black magic lol
			transform.parent.parent.GetComponent<AudioSource>().DOPitch(0.75f, 1.5f);
		}
		StartCoroutine("EndGame");
	}
	public void DoVictory() {
		sprite.sprite = winScreen;
		audHand.PlayClip("win");
		GoodNight();
	}
	public void StartGame() {
		SceneManager.LoadScene("Main Game");
	}
	protected IEnumerator EndGame() {
		yield return new WaitForSeconds(4.5f);
		SceneManager.LoadScene("Main Menu");
	}
}
