using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : EntityBase {
    public BlueprintConfig blueprint;

	// Use this for initialization
	void Start () {
        // 输入插槽的数量对应蓝图中的需求数量
        inSlots = new Slot[blueprint.inEntities.Length];

        // 初始化插槽
        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            inSlots[i] = new Slot() {
                entity = Globals.entityManager.GetEntity(blueprint.inEntities[i].id)
            };
        }
        outSlot = new Slot {
            entity = Globals.entityManager.GetEntity(blueprint.outEntity.id)
        };
    }

	// Update is called once per frame
	void Update () {
        if (isWorking) {
            workingTime += workSpeed * Time.deltaTime;
        }
        else {
            workingTime = 0f;
        }

        if (workingTime > blueprint.time) {
            workingTime -= blueprint.time;
            resetWorkingState();
            if (isWorking) {
                produce();
                resetWorkingState();
            }
        }
    }

    // 重设当前的工作状态，在输入输出有变化时调用
    public void resetWorkingState() {
        isWorking = true;

        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            if (inSlots[i].num < blueprint.inNums[i]) {
                isWorking = false;
                break;
            }
        }
    }

    // 产出产品
    void produce() {
        Globals.entityManager.produceEntity(blueprint, inSlots, outSlot);
        outSlot.instantiateEntity(false, transform.position);
    }

    public void pushEntity(GameObject gameObject) {
        Entity entity = Globals.entityManager.getEntityByGameObject(gameObject);

        for (int i = 0; i < inSlots.Length; i++) {
            if (inSlots[i].entity.id == entity.id) {
                inSlots[i].pushEntity(gameObject);
                // 有新的材料进入，重设一下工作状态
                resetWorkingState();
                break;
            }
        }
    }
}
