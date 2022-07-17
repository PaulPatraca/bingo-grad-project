using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyParallax : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        layerize(transform, gameObject.layer);
    }
    public void layerize(Transform parent, int layer) {
        if (parent.childCount > 0) {
            foreach (Transform child in parent) {
                child.gameObject.layer = layer;
                layerize(child, layer);
            }
        }
    }
}
