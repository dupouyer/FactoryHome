using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    List<Collider> colliders = new List<Collider>();

    Animator animator;
    int IDLE_HASH;

    Transform arm;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        IDLE_HASH = Animator.StringToHash("Idle");
        arm = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == IDLE_HASH && colliders.Count > 0) {
            Collider c = colliders[0];
            //c.transform.SetParent(arm);
            animator.Play(Animator.StringToHash("Run"));
            colliders.Remove(c);
        }
	}

    private void OnTriggerEnter(Collider other) {
        colliders.Add(other);
    }

    private void OnTriggerExit(Collider other) {
        colliders.Remove(other);
    }
}
