using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainerHandler : MonoBehaviour
{
	public Sprite[] hearts = new Sprite[5];
	private Transform child;
	public bool animated = false;
	private Image heartOn;
	private Image heartOff;
	public void Awake() {
		child = transform.GetChild(0);
		heartOn = child.GetComponent<Image>();
		heartOff = GetComponent<Image>();
	}
	public void triggerHeart(bool onoff) {
		if(!child)
			child = transform.GetChild(0);
		heartOff.enabled = !onoff;
		heartOn.enabled = onoff;
	}
	public IEnumerator animateHeart(int maxHealth, int damageLevel) {
		int i = -1;
		animated = true;
		while(true) {
			i = (i + 1) % hearts.Length;
			float length = 0.1f;
			if (i == 0)
				length = 1.5f/(damageLevel + 1);
			heartOn.sprite = hearts[i];
			yield return new WaitForSeconds(length);
		}
	}
}
