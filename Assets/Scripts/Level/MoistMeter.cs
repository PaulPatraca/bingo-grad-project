using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoistMeter : MonoBehaviour {
	void Update() {
        transform.Translate(new Vector3(0.25f * Time.deltaTime, 0, 0));
	}
}
