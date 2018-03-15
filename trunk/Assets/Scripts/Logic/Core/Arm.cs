using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour {
    List<Collider> colliders = new List<Collider>();

    Animator animator;

    int IDLE_HASH;
    int RUN_HASH;

    int HAS_CARGO_ENUM;
    bool hasCargo = false;

    Transform arm;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        IDLE_HASH = Animator.StringToHash("Idle");
        RUN_HASH = Animator.StringToHash("Run");
        HAS_CARGO_ENUM = Animator.StringToHash("hasCargo");
        arm = transform.GetChild(0);
	}

    // Update is called once per frame
    void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == IDLE_HASH && colliders.Count > 0 && !hasCargo) {
            Collider c = colliders[0];
            c.transform.SetParent(arm);
            hasCargo = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == RUN_HASH && hasCargo) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) {
                hasCargo = false;
                arm.GetChild(0).transform.SetParent(null);
            }
        }
            animator.SetBool(HAS_CARGO_ENUM, hasCargo);
	}

    private void OnTriggerEnter(Collider other) {
        colliders.Add(other);
    }

    private void OnTriggerExit(Collider other) {
        colliders.Remove(other);
    }
}
