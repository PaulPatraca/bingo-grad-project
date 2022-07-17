using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignRotator : MonoBehaviour {
    [Range(0, 15)]
    public float limit = 7.0f;
    [Range(0, 180)]
    public float maxAngle = 45.0f;
    [Range(0, 7)]
    public float offset = 0f;
    void Update() {
        transform.rotation = Quaternion.Euler(Mathf.Sin(Mathf.PI * 2 * (((Time.time + offset) % limit) / limit)) * maxAngle, 90, 0);
    }
}
