using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDad : MonoBehaviour
{
	[HideInInspector]
	public AudioHandler audHand;
	private GameObject kid;
	public void Start() {
		audHand = GetComponent<AudioHandler>();
		kid = transform.GetChild(0).gameObject;
	}
	public void ToggleKid() {
		if(kid.activeSelf) {
			kid.SetActive(false);
			audHand.PlayClip("close");
		} else {
			kid.SetActive(true);
			audHand.PlayClip("open");
		}
	}
}
