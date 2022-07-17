using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudinated : MonoBehaviour
{
    public float cloudVelocity;
    // Update is called once per frame
    public Sprite[] cloudSprites = new Sprite[0];
    private SpriteRenderer sprite;
	private void Awake() {
        sprite = GetComponent<SpriteRenderer>();
	}
	void Update() {
        transform.Translate(new Vector3(cloudVelocity * Time.deltaTime, 0, 0));
    }
    public void changeCloud(int index) {
        sprite.sprite = cloudSprites[index];
	}
}
