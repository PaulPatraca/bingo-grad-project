using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CoinHandler : MonoBehaviour
{
    public Text coinText;
    private bool coinCounting = false;
    private int coinsToCount = 0;
    private int countedCoins = 0;
    public GameObject shop;

    [HideInInspector]
    public AudioHandler audHand;
    public void Start() {
        audHand = GetComponent<AudioHandler>();
        updateCoins();
	}
    public void addCoins(int amnt) {
        coinsToCount += amnt;
        if(!coinCounting) {
            StartCoroutine("animateCount");
            coinCounting = true;
        } else {
            StopCoroutine("animateCount");
            countedCoins -= 1;
            StartCoroutine("animateCount");
        }
	}
    private IEnumerator animateCount() {
        while(coinsToCount > 0) {
            countedCoins += 1;
            updateCoins();
            if (coinsToCount != 1)
                yield return new WaitForSeconds(0.25f / coinsToCount);
            else
                yield return new WaitForSeconds(0.25f);
            coinsToCount -= 1;
        }
        coinCounting = false;
    }
    public void setNewCoin(int coinCount) {
        coinsToCount = 0;
        countedCoins = coinCount;
        updateCoins();
	}
	public void updateCoins() {
        coinText.text = "" + countedCoins;
	}
    public void toggleShop() {
        if (shop.activeSelf) {
            shop.SetActive(false);
            audHand.PlayClip("close");
        } else {
            shop.SetActive(true);
            shop.GetComponent<ShopHandler>().toggleShop();
            audHand.PlayClip("open");
        }
    }
}
