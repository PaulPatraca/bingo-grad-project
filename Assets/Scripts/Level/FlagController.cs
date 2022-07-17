using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagController : MonoBehaviour
{
    private Collider2D flagCol;
    public Vector3 respawnPos;
	private AudioSource audSrc;
    // Start is called before the first frame update
    void Start() {
        flagCol = GetComponent<Collider2D>();
        respawnPos = new Vector3(flagCol.offset.x, flagCol.offset.y, 0) + transform.position;
		audSrc = GetComponent<AudioSource>();
	}
	private void OnTriggerEnter2D(Collider2D other) {
		GameObject coll = other.transform.gameObject;
		if (coll.CompareTag("Player")) {
			flagCol.enabled = false;
			other.GetComponent<RespawnBacon>().respawnPos = new Vector3(respawnPos.x, respawnPos.y, other.transform.position.z);
			GetComponent<Animator>().SetTrigger("raiseFlag");
			audSrc.Play();
			enabled = false;
		}
	}
}
