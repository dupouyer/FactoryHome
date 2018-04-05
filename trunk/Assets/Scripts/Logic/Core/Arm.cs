using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : EntityBase {
    Animator animator;

    int IDLE_HASH;
    int RUN_HASH;

    int HAS_CARGO_ENUM;

    // 抓取货物的位置
    Transform fingerPoint;
    HitBox cargoBuffer;
    HitBox target;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        IDLE_HASH = Animator.StringToHash("Idle");
        RUN_HASH = Animator.StringToHash("Run");
        HAS_CARGO_ENUM = Animator.StringToHash("hasCargo");
        fingerPoint = transform.GetChild(0);

        cargoBuffer = transform.GetChild(1).GetComponent<HitBox>();
        target = transform.GetChild(2).GetComponent<HitBox>();
	}

    // Update is called once per frame
    void Update() {
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == IDLE_HASH && cargoBuffer.hit.Count > 0 && !isWorking) {
            GameObject cargo = cargoBuffer.hit[0];
            cargoBuffer.hit.Remove(cargo);
            cargo.GetComponent<Collider>().enabled = false;
            // 货物绑定在手指上
            cargo.transform.SetParent(fingerPoint);
            isWorking = true;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == RUN_HASH && isWorking ) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) {
                isWorking = false;
                GameObject cargo = fingerPoint.GetChild(0).gameObject;
                // 从手指上移除
                cargo.transform.SetParent(null);

                if (target.hit.Count > 0) {
                    Producer p = target.hit[0].GetComponent<Producer>();
                    p.pushEntity(cargo);
                }
                else {
                    cargo.GetComponent<Collider>().enabled = true;
                }
            }
        }

        animator.SetBool(HAS_CARGO_ENUM, isWorking);
	}
}
