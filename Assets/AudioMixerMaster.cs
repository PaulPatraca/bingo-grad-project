using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class AudioMixerMaster : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioHandler handler;
	private Slider[] sliders = new Slider[3];
	public void Start() {
        int i = 0;
        foreach (Transform child in transform) {
            if (i == 3)
                break;
            sliders[i] = child.GetComponent<Slider>();
            i++;
        }
        sliders[0].onValueChanged.AddListener(MasterChange);
        sliders[1].onValueChanged.AddListener(EffectsChange);
        sliders[2].onValueChanged.AddListener(MusicChange);
    }
    public void MasterChange(float value) {
        if (value == -40.0f)
            value = -80.0f;
        mixer.SetFloat("MasterVolume", value);
    }
    public void EffectsChange(float value) {
        if (value == -40.0f)
            value = -80.0f;
        mixer.SetFloat("EffectsVolume", value);
    }
    public void MusicChange(float value) {
        if (value == -40.0f)
            value = -80.0f;
        mixer.SetFloat("MusicVolume", value);
    }
    public void CloseMenu() {
        handler.PlayClip("close");
        gameObject.SetActive(false);
    }
}
