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
        // 拿取货物
        if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == IDLE_HASH && cargoBuffer.length > 0 && !isWorking) {
            GameObject cargo;
            if (cargoBuffer.materials.Count > 0) {
                cargo = cargoBuffer.materials[0];
                cargoBuffer.materials.Remove(cargo);
                EntityBase e = cargo.GetComponent<EntityBase>();
                if (e is Materail && (e as Materail).transport) {
                    (e as Materail).transport.drawCargo(e);
                }
            }
            else {
                EntityBase box = cargoBuffer.entity;
                cargo = box.outSlot.instantiateEntity(false, box.gameObject.transform.position, false);
            }

            cargo.GetComponent<Collider>().enabled = false;
            // 货物绑定在手指上
            cargo.transform.SetParent(fingerPoint);
            cargo.transform.localPosition = Vector3.zero;
            isWorking = true;
        }
        // 放置货物
        else if (animator.GetCurrentAnimatorStateInfo(0).shortNameHash == RUN_HASH && isWorking ) {
            if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f) {
                // 新加入的货物是第三个 child
                GameObject cargo = fingerPoint.GetChild(2).gameObject;

                // 目标位置有实体，尝试放入实体中
                if (target.entity) {
                    if (target.entity.pushEntity(cargo)) {
                        isWorking = false;
                    }
                    else {
                        // 在下一帧继续尝试
                    }
                }
                else {
                    cargo.GetComponent<Collider>().enabled = true;
                    // 从手指上移除
                    cargo.transform.SetParent(null);
                    isWorking = false;
                }
            }
        }

        animator.SetBool(HAS_CARGO_ENUM, isWorking);
	}
}
