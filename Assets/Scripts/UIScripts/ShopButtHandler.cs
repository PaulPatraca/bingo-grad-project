using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButtHandler : MonoBehaviour
{
    public Image icon;
    public Text label;
    public Button button;
    public int index;
    public int price;
    private ShopHandler handler;
    void Awake() {
        icon = GetComponent<Image>();
        label = GetComponentInChildren<Text>();
        button = GetComponent<Button>();
        handler = GetComponentInParent<ShopHandler>();
	}
    public void SetPrice(int amnt) {
        price = amnt;
        label.text = "" + amnt;
        if (amnt == 0)
            label.text = "-";
        SetEnabled(amnt != 0);
	}
    public void SetEnabled(bool state){
        state = state && price != 0;
        button.enabled = state;
        Color newColor = Color.white;
        if(!state)
            newColor = Color.gray;
        GetComponent<CanvasRenderer>().SetColor(newColor);
        label.GetComponent<CanvasRenderer>().SetColor(newColor);
        foreach (Transform child in transform) {
            child.gameObject.GetComponent<CanvasRenderer>().SetColor(newColor);
        }
    }
}
