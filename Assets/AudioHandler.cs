using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioHandler : MonoBehaviour
{
    private AudioSource audSrc;
    public List<string> sounds;
    public List<AudioClip> audios;
    [HideInInspector]
    public Dictionary<string, AudioClip> soundDict;
    void Start() {
        audSrc = GetComponent<AudioSource>();
        soundDict = new Dictionary<string, AudioClip>();
        for(int i = 0; i < sounds.Count; i++)
            soundDict[sounds[i]] = audios[i];
    }
    public void PlayClip(string name) {
        PlayClip(name, 1.0f);
    }
    public void PlayClip(string name, float amnt) {
        audSrc.PlayOneShot(soundDict[name],amnt);
    }
}
