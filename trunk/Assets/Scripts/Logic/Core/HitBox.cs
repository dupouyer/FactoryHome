using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour {
    // 碰撞物
    public List<GameObject> hit = new List<GameObject>();

	// Use this for initialization
	void Start () {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other) {
        hit.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other) {
        hit.Remove(other.gameObject);
    }
}
