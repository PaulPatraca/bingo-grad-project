using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Collectable : MonoBehaviour {
	protected SpriteRenderer sprite;
	protected Collider2D col;
	protected Transform pos;
	protected Vector3 origPosLoc;
	protected Vector3 origPos;
	protected void Start() {
		sprite = GetComponent<SpriteRenderer>();
		col = GetComponent<Collider2D>();
		pos = GetComponent<Transform>();
		origPosLoc = pos.localPosition;
		origPos = pos.position;
	}
	protected void Update() {
		pos.localPosition = new Vector3(origPosLoc.x, origPosLoc.y + Mathf.Sin((Time.time % (Mathf.PI * 2)) *2)/4, origPosLoc.z);
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject coll = other.transform.gameObject;
		if(coll.CompareTag("Player")) {
			col.enabled = false;
			sprite.DOColor(new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0), 0.5f);
			pos.DOMove(pos.position + new Vector3(0, 1.0f, 0), 0.5f);
			StartCoroutine("ClearItem", other.transform.gameObject);
		}
	}
	protected IEnumerator ClearItem(GameObject other) {
		DoEffect(other);
		yield return new WaitForSeconds(3.5f);
		Destroy(gameObject);
	}
	protected virtual void DoEffect(GameObject other) {
	}
}
