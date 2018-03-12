using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 搬运组件
public class Transport : MonoBehaviour {
    // speed of transport
    public float speed = 0.3f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other) {
        Debug.Log("onTrigger");
        other.transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    private void OnCollisionStay(Collision collision) {
        Debug.Log("onCollision");
    }
}
