using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        Debug.Log("trigger enter");
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("trigger stay");
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("trgger exit");
    }

}
