using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour {
	private Collision coll;
	[HideInInspector]
	public Rigidbody2D rb;
	private PlayerAnimator anim;
	private PlayerMain play;

	[Space]
	[Header("Stats")]
	public float speed = 10;
	public float jumpForce = 50;
	public float slideSpeed = 5;
	public float wallJumpLerp = 10;
	public float dashSpeed = 20;

	[Space]
	[Header("Booleans")]
	public bool canMove;
	public bool wallGrab;
	public bool wallJumped;
	public bool wallSlide;
	public bool isDashing;

	[Space]

	private bool groundTouch;
	private bool hasDashed;

	public int side = 1;

	private float timeSinceLastJingle = 0.0f;
	//this is some cosmic fuckery.
	public float jingleTimer = 1.0f;


	// Start is called before the first frame update
	void Start() {
		coll = GetComponent<Collision>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<PlayerAnimator>();
		play = GetComponent<PlayerMain>();
	}

	// Update is called once per frame
	void Update() {
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		float xRaw = Input.GetAxisRaw("Horizontal");
		float yRaw = Input.GetAxisRaw("Vertical");
		Vector2 dir = new Vector2(x, y);

		if(!coll.onWall || (coll.onLeftWall && x > 0) || (coll.onRightWall && x < 0))
		Walk(dir);

		if (wallGrab && !isDashing) {
			rb.gravityScale = 0;
			if (x > .2f || x < -.2f)
				rb.velocity = new Vector2(rb.velocity.x, 0);

			float speedModifier = y > 0 ? .5f : 1;

			rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
		} else {
			rb.gravityScale = 3;
		}

		if (coll.onWall && !coll.onGround) {
			if (x != 0 && !wallGrab) {
				wallSlide = true;
			}
		}

		if (!coll.onWall || coll.onGround)
			wallSlide = false;

		if (Input.GetButtonDown("Jump")) {

			if (coll.onGround) {
				anim.SetTrigger("Jumped");
				anim.player.audHand.PlayClip("jump", 0.125f);
				Jump(Vector2.up, false);
			}
		}

		if (coll.onGround && !groundTouch) {
			GroundTouch();
			groundTouch = true;
		}

		if (!coll.onGround && groundTouch) {
			groundTouch = false;
		}


		if (wallGrab || wallSlide || !canMove)
			return;

		if (x > 0) {
			side = 1;
			anim.FlipSprite(side);
		}
		if (x < 0) {
			side = -1;
			anim.FlipSprite(side);
		}
		if(coll.onGround && timeSinceLastJingle < Time.time && x != 0 && anim.player.hasBell) {
			timeSinceLastJingle = Time.time + jingleTimer;
			anim.player.audHand.PlayClip("jingle", 0.5f);
		}
	}

	void GroundTouch() {
	}
	private void Walk(Vector2 dir) {
		if (!canMove)
			return;

		if (Input.GetKey(KeyCode.LeftShift) && play.hasBoot && coll.onGround)
			rb.velocity = new Vector2(dir.x * speed * 1.5f, rb.velocity.y);
		else
			rb.velocity = new Vector2(dir.x * Mathf.Max(speed, Mathf.Abs(rb.velocity.x)), rb.velocity.y);

	}

	private void Jump(Vector2 dir, bool wall) {
		rb.velocity = new Vector2(rb.velocity.x, 0);
		rb.velocity += dir * jumpForce;
	}

	IEnumerator DisableMovement(float time) {
		canMove = false;
		yield return new WaitForSeconds(time);
		canMove = true;
	}

	void RigidbodyDrag(float x) {
		rb.drag = x;
	}

}
