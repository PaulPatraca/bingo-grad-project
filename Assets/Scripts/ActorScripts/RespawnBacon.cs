using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//mmm bacon.
public class RespawnBacon : MonoBehaviour {
    public Vector3 respawnPos;
    public GameObject respawnObject;
    public GameObject respawnEffect;
    public GameObject sploosh;
    [Range(0.0f,5.0f)]
    public float respawnTimer = 1.5f;
    public bool drowning = false;

    private Rigidbody2D body;
    private PlayerMain player;
    private Doginator dog;
    private Movement move;
    private SpriteRenderer drawer;
	private void Start() {
        body = GetComponent<Rigidbody2D>();
        player = GetComponent<PlayerMain>();
        move = GetComponent<Movement>();
        drawer = GetComponentInChildren<SpriteRenderer>();
        dog = GetComponentInChildren<Doginator>();
    }
	public void Update() {
		if(!drowning && transform.position.y < -4.81) {
            drowning = true;
            drawer.enabled = false;
            body.velocity = new Vector2(0, body.velocity.y);
            if(player != null) {
                player.damage(1);
                move.canMove = false;
                player.audHand.PlayClip("splash");
                if(player.maxHealth != player.damageLevel)
                    DoRespawn();
            }
            else {
                if (dog != null && dog.enabled)
                    dog.handler.PlayClip("splash");
                DoRespawn();
            }
        }
	}
	public void DoRespawn() {
        Instantiate(sploosh, new Vector3(transform.position.x, -3.8f, sploosh.transform.position.z), transform.rotation);
        StartCoroutine("Respawn");
    }
    protected IEnumerator Respawn() {
        yield return new WaitForSeconds(respawnTimer);
        drawer.enabled = true;
        if (respawnEffect != null)
            Instantiate(respawnEffect, respawnPos + respawnEffect.transform.position, transform.rotation);
        if(respawnObject != null) {
            Instantiate(respawnObject, respawnPos + respawnObject.transform.position, transform.rotation);
            drawer.enabled = false;
            yield return new WaitForSeconds(1.5f);
            Destroy(gameObject);
        }
        transform.position = respawnPos;
        body.velocity = new Vector2(0, 0);
        drowning = false;
        if (move != null)
            move.canMove = true;
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(respawnPos, 0.5f);
    }
}
