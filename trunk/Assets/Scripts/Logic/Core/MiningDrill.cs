using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : EntityBase {
    public EntityConfig ore;

    // 出口碰撞检查
    HitBox outHitBox;
    public Transport outTransport;

    Animator animator;

    int WORK_HASH = Animator.StringToHash("isWorking");

	// Use this for initialization
	void Start () {
        // for test
        isWorking = true;
        outSlot = new Slot();
        outHitBox = GetComponentInChildren<HitBox>();

        outHitBox.onTriggerTransport += delegate(Transport transport) {
            outTransport = outHitBox.transport;
        };

        animator = GetComponentInChildren<Animator>();
	}

    // Update is called once per frame
    void Update () {
        if (isWorking && ore && outTransport) {
            animator.SetBool(WORK_HASH, true);
            workingTime += Time.deltaTime;
            if (workingTime >= workSpeed) {
                workingTime -= workSpeed;
                produce();
            }
        }
        else {
            animator.SetBool(WORK_HASH, false);
        }
	}

    public void setOre(EntityConfig ore) {
        this.ore = ore;
        isWorking = true;
    }

    void produce() {
        Globals.entityManager.CreateToSlot(ore.id, 1, outSlot);
        EntityBase oreEntity = outSlot.instantiateEntity(false, transform.position + Vector3.forward, false).GetComponent<EntityBase>();
        Vector3 offset = Vector3.right * Random.Range(-0.25f, 0.25f);
        Vector3 p = outHitBox.transform.TransformPoint(offset);
        outTransport.pushCargo(oreEntity, p);
        oreEntity.gameObject.GetComponent<Collider>().enabled = true;
    }
}
