using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour {
    public GameObject outFactor;

    bool isWorking = false;

    int need;

    float progress = 0f;

    float speed = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        isWorking = need > 0;

        if (isWorking) {
            progress += speed * Time.deltaTime;
            Debug.Log("progress:" + progress);
        }

        if (progress >= 1.0f && isWorking) {
            need -= 1;
            progress = 0;

            OutOne();
        }
	}

    void OutOne() {
        GameObject one = Instantiate(outFactor);
        one.transform.position = transform.position + Vector3.forward;
    }

    private void OnTriggerEnter(Collider other) {
        need += 1;
        Destroy(other.gameObject);
    }

}
