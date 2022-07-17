using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PlayerAnimator : MonoBehaviour {
	[HideInInspector]
	public Animator anim;
	private SpriteRenderer spriteRenderer;
	private Movement move;
	private Collision coll;
	private Rigidbody2D rb;
	[HideInInspector]
	public PlayerMain player;
	private float timeSinceLastInput = 0.0f;

	public SpriteRenderer sweater;
	private Dictionary<string, Sprite> sweaterImages = new Dictionary<string, Sprite>();

	public SpriteRenderer collar;
	private Dictionary<string, Sprite> collarImages = new Dictionary<string, Sprite>();

	public SpriteRenderer boot;
	private Dictionary<string, Sprite> bootImages = new Dictionary<string, Sprite>();
	// Start is called before the first frame update
	void Start() {
		anim = GetComponentInParent<Animator>();
		spriteRenderer = GetComponentInParent<SpriteRenderer>();
		move = GetComponentInParent<Movement>();
		rb = GetComponentInParent<Rigidbody2D>();
		coll = GetComponentInParent<Collision>();
		player = GetComponentInParent<PlayerMain>();

		var sprites = Resources.LoadAll("sweater", typeof(Sprite)).Cast<Sprite>();
		foreach (var t in sprites) {
			sweaterImages[t.name.Substring(t.name.IndexOf("_") + 1)] = t;
		}
		sprites = Resources.LoadAll("collar", typeof(Sprite)).Cast<Sprite>();
		foreach (var t in sprites) {
			collarImages[t.name.Substring(t.name.IndexOf("_") + 1)] = t;
		}
		sprites = Resources.LoadAll("boot", typeof(Sprite)).Cast<Sprite>();
		foreach (var t in sprites) {
			bootImages[t.name.Substring(t.name.IndexOf("_") + 1)] = t;
		}
	}
	private void Update() {
		Sprite currentFrame = spriteRenderer.sprite;
		sweater.sprite = sweaterImages[currentFrame.name];
		sweater.flipX = spriteRenderer.flipX;
		collar.sprite = collarImages[currentFrame.name];
		collar.flipX = spriteRenderer.flipX;
		boot.sprite = bootImages[currentFrame.name];
		boot.flipX = spriteRenderer.flipX;

		var xspeed = Mathf.Log(Mathf.Abs(rb.velocity.x), move.speed);
		var yspeed = rb.velocity.y;
		anim.SetFloat("xSpeed", xspeed != Mathf.NegativeInfinity ? xspeed : 0);
		anim.SetFloat("ySpeed", yspeed);
		anim.SetBool("OnGround", coll.onGround);
		if(Input.anyKey)
			timeSinceLastInput = Time.time;
		anim.SetBool("ColdBoy", Time.time - timeSinceLastInput > 3.0f);
		anim.SetBool("FlinchInvuln", player.invuln);

	}
	private void FixedUpdate() {
		Sprite currentFrame = spriteRenderer.sprite;
		sweater.sprite = sweaterImages[currentFrame.name];
		sweater.flipX = spriteRenderer.flipX;
		collar.sprite = collarImages[currentFrame.name];
		collar.flipX = spriteRenderer.flipX;
		boot.sprite = bootImages[currentFrame.name];
		boot.flipX = spriteRenderer.flipX;
	}
	public void SetTrigger(string name) {
		anim.SetTrigger(name);
	}
	public void FlipSprite(int side) {
		bool state = side != 1;
		spriteRenderer.flipX = state;
	}
}
