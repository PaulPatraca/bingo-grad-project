using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopHandler : MonoBehaviour {

    public GameObject[] buttonsGO = new GameObject[6];
    private ShopButtHandler[] buttons = new ShopButtHandler[6];
    private bool[] purchased = new bool[6];
    public int[] prices = new int[6];

    [Space]
    public GameObject sweaterButton;
    public GameObject collarButton;
    public GameObject bootButton;

    [Space]
    public PlayerMain player;
    private GameObject panel;
    public CatHouse house;
    public bool realEstatePurchased = false;
    // Start is called before the first frame update
    void Start() {
        panel = gameObject;
        for(int i = 0; i < prices.Length; i ++) {
            buttons[i] = buttonsGO[i].transform.GetComponent<ShopButtHandler>();
            buttons[i].index = i;
            buttons[i].SetPrice(prices[i]);
            purchased[i] = false;
            buttons[i].SetEnabled(false);
		}
        buttons[0].SetEnabled(true);
    }
    public void toggleButtons() {
        for (int i = 0; i < prices.Length; i++) {
            if (buttons[i]) {
                if (!realEstatePurchased && i == 0)
                    buttons[i].SetEnabled(true);
                else {
                    if (!realEstatePurchased)
                        buttons[i].SetEnabled(false);
                    else
                        buttons[i].SetEnabled(!purchased[i]);
                }
            }
        }
    }
    public void toggleShop() {
        toggleButtons();
    }
    public bool canBuy(int amnt) {
        bool canBuy = realEstatePurchased && amnt <= player.coins;
        if (canBuy)
            player.audHand.PlayClip("buy");
        return canBuy;
	}
    public void buyHouse(int amnt) {
        amnt = prices[amnt];
        if(amnt <= player.coins) {
            player.audHand.PlayClip("buy");
            realEstatePurchased = true;
            house.toggleHouse();
            player.loseCoins(amnt);
            buttons[0].SetEnabled(false);
            purchased[0] = true;
            for (int i = 1; i < prices.Length; i++)
                buttons[i].SetEnabled(true);
        }
	}
    public void buyContainer(int amnt) {
        amnt = prices[amnt];
        if (canBuy(amnt)) {
            player.increaseMaxHealth(1);
            player.loseCoins(amnt);
            purchased[1] = true;
            toggleButtons();
        }
    }
    public void buySweater(int amnt) {
        amnt = prices[amnt];
        if (canBuy(amnt)) {
            purchased[2] = true;
            player.loseCoins(amnt);
            sweaterButton.SetActive(true);
            toggleButtons();
            player.activateChild(1);
        }
    }
    public void buyCollar(int amnt) {
        amnt = prices[amnt];
        if (canBuy(amnt)) {
            purchased[3] = true;
            player.loseCoins(amnt);
            collarButton.SetActive(true);
            toggleButtons();
            player.activateChild(0);
            player.hasBell = true;
        }
    }
    public void buyBoots(int amnt) {
        amnt = prices[amnt];
        if (canBuy(amnt)) {
            purchased[4] = true;
            player.loseCoins(amnt);
            bootButton.SetActive(true);
            toggleButtons();
            player.activateChild(2);
            player.hasBoot = true;
        }
    }
    public void toggleSweater() {
        player.toggleChild(1);
    }
    public void toggleCollar() {
        player.hasBell = !player.toggleChild(0);
    }
    public void toggleBoot() {
        player.hasBoot = !player.toggleChild(2);
    }
}
