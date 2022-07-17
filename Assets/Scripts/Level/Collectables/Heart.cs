using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
	public int value = 1;
	public AudioSource audSrc;
	private new void Start() {
		base.Start();
		audSrc = GetComponent<AudioSource>();
	}
	protected override void DoEffect(GameObject other) {
		PlayerMain player = other.GetComponent<PlayerMain>();
		player.heal(value);
		audSrc.PlayOneShot(audSrc.clip);
	}
}
