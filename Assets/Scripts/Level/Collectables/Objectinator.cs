using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectinator : MonoBehaviour {
    public GameObject[] objects;
    public Vector3[] calculatedPositions;
    public Vector3 sizing = new Vector3();
    public enum NumaNuma {FLAT, UPARC, DOWNARC};
    public NumaNuma state = NumaNuma.FLAT;
    // Start is called before the first frame update
    void Start() {
        BuildObjects();
    }
    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        if(calculatedPositions != null) {
            for (int i = 0; i < calculatedPositions.Length; i++) {
                if(objects[i] != null)
                    Gizmos.DrawWireSphere(transform.position + calculatedPositions[i] + new Vector3(0, 0.5f), 0.5f);
            }
            switch(state) {
                case NumaNuma.FLAT:
                    Gizmos.DrawWireCube(transform.position + new Vector3(0, 0.5f), new Vector3(sizing.x, 1, 1));
                    break;
                case NumaNuma.UPARC:
                    Gizmos.DrawWireCube(transform.position + new Vector3(0, (sizing.y) / 2.0f + 0.5f), new Vector3(sizing.x, sizing.y, 1));
                    break;
                case NumaNuma.DOWNARC:
                    Gizmos.DrawWireCube(transform.position + new Vector3(0, (sizing.y) / 2.0f + 0.5f - sizing.y), new Vector3(sizing.x, sizing.y, 1));
                    break;
            }
        }
    }
    void BuildObjects() {
        Vector3 shift = new Vector3(0, 0.5f, 0);
        for(int i = 0; i < objects.Length; i ++) {
            if(objects[i] != null) {
                GameObject child = Instantiate(objects[i], transform);
                child.transform.localPosition += calculatedPositions[i] + shift;
            }
        }
    }
    private void OnValidate() {
        if (objects != null && objects.Length > 0) {
            int length = objects.Length;
            calculatedPositions = new Vector3[length];
            float leftMost = sizing.x / -2;
            float stepWidth = sizing.x / (length - 1);
            switch (state) {
                case NumaNuma.FLAT: {
                    for (int i = 0; i < length; i++) {
                        calculatedPositions[i] = new Vector3(leftMost + stepWidth*(i), 0, 0);
                    }
                    break;
                }
                case NumaNuma.UPARC: {
                    for (int i = 0; i < length; i++) {
                        calculatedPositions[i] = new Vector3(Mathf.Cos(Mathf.PI * (i / (length - 1.0f))) * leftMost, Mathf.Sin(Mathf.PI * (i / (length - 1.0f))) * sizing.y, 0);
                    }
                    break;
                }
                case NumaNuma.DOWNARC: {
                    for (int i = 0; i < length; i++) {
                        calculatedPositions[i] = new Vector3(Mathf.Cos(Mathf.PI * (i / (length - 1.0f))) * leftMost, Mathf.Sin(Mathf.PI + Mathf.PI * (i / (length - 1.0f))) * sizing.y, 0);
                    }
                    break;
                }
            }
        }
    }
}
