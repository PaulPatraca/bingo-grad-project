using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenuUIHandler : MonoBehaviour
{
    public Button[] buttons = new Button[4];
    public GameObject soundMenu;
    void Start() {
        int i = 0;
        foreach(Transform child in transform) {
            buttons[i] = child.GetComponent<Button>();
            i++;
		}
        buttons[0].onClick.AddListener(ResetGame);
        buttons[1].onClick.AddListener(SoundSettings);
        buttons[2].onClick.AddListener(MainMenu);
        buttons[3].onClick.AddListener(CloseMenu);
    }
    public void ResetGame() {
        SceneManager.LoadScene("Main Game");
    }
    public void SoundSettings() {
        gameObject.SetActive(false);
        soundMenu.SetActive(true);
    }
    public void MainMenu() {
        SceneManager.LoadScene("Main Menu");
    }
    public void CloseMenu() {
        gameObject.SetActive(false);
        GetComponentInParent<AudioHandler>().PlayClip("close");
	}
}
