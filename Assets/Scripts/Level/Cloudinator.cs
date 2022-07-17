using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloudinator : MonoBehaviour
{
    public GameObject cloudPrefab;
    public int seed;
    public float[] cloudWeights;
    public int[] cloudCount;
    // Start is called before the first frame update
    void Start() {
        Random.InitState(seed);
        int count = cloudPrefab.GetComponent<Cloudinated>().cloudSprites.Length;
        cloudWeights = new float[count + 1];
        cloudCount = new int[count];
        for (int i = 0; i < count + 1; i++)
            cloudWeights[i] = 1.0f / count * i;
        for(int i = 0; i < 100; i ++) {
            GameObject cloud = Instantiate(cloudPrefab);
            cloud.transform.SetParent(transform);
            cloud.transform.localPosition = new Vector3(-50.0f + 10.0f * i + Random.Range(-1.5f, 1.5f), Random.Range(-4f, 10f), Random.Range(-8f, 16f));
            float random = Random.Range(0.0f, 1.0f);
            for(int j = 0; j < count; j ++) {
                if (cloudWeights[j] < random && random <= cloudWeights[j + 1]) {
                    cloud.GetComponent<Cloudinated>().changeCloud(j);
                    cloudCount[j]++;
                }
			}
        }
    }
}
