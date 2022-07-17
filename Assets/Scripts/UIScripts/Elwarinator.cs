using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Elwarinator : MonoBehaviour
{
    public float frameRate = 24;
    public Sprite[] frames = new Sprite[48];
    private int frame = 0;
    private float secondPerFrame = 0.0f;
    private Image elwar;
    // Start is called before the first frame update
    void OnEnable() {
        secondPerFrame = 1/frameRate;
        elwar = GetComponent<Image>();
        StartCoroutine("animateElwar");
	}
	public IEnumerator animateElwar() {
		while (true) {
            elwar.sprite = frames[frame];
            yield return new WaitForSeconds(secondPerFrame);
            frame = (frame + 1) % frames.Length;
		}
	}

}
