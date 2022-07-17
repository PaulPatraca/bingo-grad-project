using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doginator : MonoBehaviour {
	public GameObject dogBoom;
	public Animator dogger;
	private Transform par;
	[HideInInspector]
	public AudioHandler handler;
	public RespawnBacon spawn;
	void Start() {
		par = transform.parent;
		dogger = par.GetComponentInChildren<Animator>();
		handler = par.GetComponent<AudioHandler>();
		spawn = par.GetComponent<RespawnBacon>();
		foreach(var anim in dogger.runtimeAnimatorController.animationClips) {
			if(anim.name == "Duck") {
				spawn.respawnTimer = anim.length;
				break;
			}
		}
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject coll = other.transform.gameObject;
		if (coll.CompareTag("Player")) {
			Rigidbody2D body = other.GetComponent<Rigidbody2D>();
			if(body.velocity.y < 0) {
				Instantiate(dogBoom, transform.position, transform.rotation);
				handler.PlayClip("die");
				dogger.SetTrigger("Stomped");
				spawn.StartCoroutine("Respawn");
				DogAInator dogAI = par.GetComponentInChildren<DogAInator>();
				dogAI.enabled = false;
				dogAI.StopAllCoroutines();
				foreach (var collider in par.GetComponentsInChildren<Collider2D>())
					collider.enabled = false;
				par.GetComponent<Collider2D>().enabled = true;
				enabled = false;
				Rigidbody2D rbody = par.GetComponent<Rigidbody2D>();
				rbody.velocity = new Vector2(0, -5f);
			} else {
				other.GetComponent<PlayerMain>().dramaticDamage(1);
			}
		}
	}
}
