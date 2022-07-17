using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingDog : MonoBehaviour
{
    public List<Animator> doggies;
    // Start is called before the first frame update
    void Start()
    {
        doggies = new List<Animator>();
        foreach (var animator in GetComponentsInChildren<Animator>())
            doggies.Add(animator);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        GameObject player = other.transform.gameObject;
        if (player.CompareTag("Player")) {
            foreach (var animator in doggies) {
                animator.SetBool("alerted", true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        GameObject player = other.transform.gameObject;
        if (player.CompareTag("Player")) {
            foreach (var animator in doggies) {
                animator.SetBool("alerted", false);
            }
        }
    }
}
