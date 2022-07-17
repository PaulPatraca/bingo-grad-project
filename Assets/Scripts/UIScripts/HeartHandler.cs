using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartHandler : MonoBehaviour
{
    public GameObject heartTemplate;
    public GameObject[] hearts;
    public HeartContainerHandler[] handlers;
    private int maxHealth = 0;
	public void Start() {
    }
    public void redrawHearts(int maxHealth) {
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
        this.maxHealth = maxHealth;
        hearts = new GameObject[maxHealth];
        handlers = new HeartContainerHandler[maxHealth];
        float spacing = float.NaN;
        for (int i = 0; i < maxHealth; i++) {
            GameObject heart = Instantiate(heartTemplate);
            RectTransform trans = heart.GetComponent<RectTransform>();
            trans.SetParent(transform);
            trans.localScale = new Vector3(trans.localScale.x * ((float) Screen.width / 1920.0f), trans.localScale.y * ((float) Screen.width / 1920.0f), trans.localScale.z);
            if (float.IsNaN(spacing))
                spacing = trans.sizeDelta.x * trans.localScale.x + 20;
            trans.localPosition = new Vector3(spacing * -i, 0, 0);
            int pos = maxHealth - i - 1;
            hearts[pos]= trans.gameObject;
            handlers[pos] = heart.GetComponent<HeartContainerHandler>();
            handlers[pos].triggerHeart(true);
        }
        for (int i = 0; i < maxHealth; i++)
            handlers[i].StartCoroutine(handlers[i].animateHeart(maxHealth, 0));
    }
    public void updateHearts(int damageLevel) {
        for (int i = 0; i < maxHealth; i++) {
            handlers[i].StopAllCoroutines();
            handlers[i].StartCoroutine(handlers[i].animateHeart(maxHealth, damageLevel));
            if (i < damageLevel) {
                handlers[i].triggerHeart(false);
            }
            else {
                handlers[i].triggerHeart(true);
            }
        }
	}
}
