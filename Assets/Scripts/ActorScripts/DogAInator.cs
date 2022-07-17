using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAInator : MonoBehaviour
{
    public GameObject target;
    private Animator anim;
    private Rigidbody2D body;
    private Transform parent;
    private Collision coll;
    private SpriteRenderer dogren;
    public Doginator dogger;

    private bool attackMode = false;

	public void Start() {
        parent = transform.parent;
        anim = parent.GetComponentInChildren<Animator>();
        body = parent.GetComponent<Rigidbody2D>();
        coll = parent.GetComponent<Collision>();
        dogren = parent.GetComponentInChildren<SpriteRenderer>();
        dogger = parent.GetComponentInChildren<Doginator>();
    }
	public void Update() {
        anim.SetFloat("ySpeed", body.velocity.y);
        anim.SetBool("Grounded", coll.onGround);
        if(body.velocity.x != 0)
            dogren.flipX = Mathf.Sign(body.velocity.x) > 1;
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject player = other.transform.gameObject;
		if (player.CompareTag("Player") && !attackMode && coll.onGround) {
			target = player;
            StartCoroutine("Attack");
            attackMode = true;
		}
    }
    protected IEnumerator Attack() {
        anim.SetTrigger("Bork");
        dogger.handler.PlayClip("bork");
        attackMode = true;
        yield return new WaitForSeconds(0.5f);
        if(coll.onGround) {
            attackMode = false;
            Vector3 pos = target.transform.position - parent.position;
            float maxHorSpeed = 10;
            float xDis = pos.x;
            float xSpeed = Mathf.Min(Mathf.Abs(maxHorSpeed), xDis);
            float vertSpeed = Mathf.Sqrt((maxHorSpeed * maxHorSpeed) - (xSpeed * xSpeed));
            body.velocity = new Vector2(xSpeed, vertSpeed);
            dogger.handler.PlayClip("jump", 0.25f);
        }
    }
}
