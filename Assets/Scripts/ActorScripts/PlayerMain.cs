using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMain : MonoBehaviour
{
    public CoinHandler coinRef;
    public HeartHandler healthRef;
    public GameEnder gameOver;
    private SpriteRenderer spriteRend;
    [HideInInspector]
    public AudioHandler audHand;

    public int damageLevel = 0;
    public int maxHealth = 3;
    public int coins = 0;
    [HideInInspector]
    public bool invuln = false;
    [Range(0.0f, 1.0f)]
    public float invulnDur = 0.5f;
    public bool hasBell = false;
    public bool hasBoot = false;
	public void Start() {
        healthRef.redrawHearts(maxHealth);
        healthRef.updateHearts(damageLevel);
        spriteRend = GetComponentInChildren<SpriteRenderer>();
        audHand = GetComponent<AudioHandler>();
	}
	// Start is called before the first frame update
	public void damage(int amnt) {
        damageLevel += amnt;
        if (damageLevel >= maxHealth) {
            damageLevel = maxHealth;
            perish();
        }
        healthRef.updateHearts(damageLevel);
    }
    public void perish() {
        gameOver.GoodNight();
    }
    public void gainCoins(int amnt) {
        coins += amnt;
        coinRef.addCoins(amnt);
	}
    public void loseCoins(int amnt) {
        coins -= amnt;
        coinRef.setNewCoin(coins);
    }
    public void heal(int amnt) {
        if (amnt == int.MaxValue)
            amnt = maxHealth;
        damageLevel -= amnt;
        if(damageLevel < 0)
            damageLevel = 0;
        healthRef.updateHearts(damageLevel);
	}
    public void increaseMaxHealth(int amnt) {
        maxHealth += 1;
        healthRef.redrawHearts(maxHealth);
        healthRef.updateHearts(damageLevel);
	}
    public void dramaticDamage(int amnt) {
        if(!invuln) {
            invuln = true;
            damage(amnt);
            StartCoroutine("Invuln");
        }
    }
    protected IEnumerator Invuln() {
        int iterations = 5;
        float timing = invulnDur / 2 / iterations;
        for (int i = 0; i < iterations; i++) {
            spriteRend.DOColor(new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 0), timing);
            yield return new WaitForSeconds(invulnDur / 2 / iterations);
            spriteRend.DOColor(new Color(spriteRend.color.r, spriteRend.color.g, spriteRend.color.b, 1.0f), timing);
            yield return new WaitForSeconds(invulnDur / 2 / iterations);
        }
        invuln = false;
    }
    public void activateChild(int index) {
        GameObject child = transform.GetChild(index).gameObject;
        child.SetActive(true);
    }
    public bool toggleChild(int index) {
        GameObject child = transform.GetChild(index).gameObject;
        child.SetActive(!child.activeSelf);
        return !child.activeSelf;
    }
    public void WinGame() {
        gameOver.DoVictory();
	}
}
