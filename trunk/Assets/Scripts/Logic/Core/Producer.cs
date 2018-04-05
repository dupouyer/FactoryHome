using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Producer : EntityBase {
    public BlueprintConfig blueprint;

    Dictionary<string, int> slotMap;

	// Use this for initialization
	void Start () {
        // 输入插槽的数量对应蓝图中的需求数量
        inSlot = new Entity[blueprint.inEntities.Length];
        slotMap = new Dictionary<string, int>();
        // 建立这个蓝图的输入和输入插槽的隐射关系
        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            slotMap.Add(blueprint.inEntities[i].id, i);
        }
	}

	// Update is called once per frame
	void Update () {
        if (isWorking) {
            workingTime += workSpeed * Time.deltaTime;

            Debug.Log("working:" + workingTime);
        }
        else {
            workingTime = 0f;
        }

        if (workingTime > blueprint.time) {
            workingTime -= blueprint.time;

            produce();
        }
    }

    // 重设当前的工作状态，在输入输出有变化时调用
    public void resetWorkingState() {
        isWorking = true;

        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            if (inSlot[i].num < blueprint.inNums[i]) {
                isWorking = false;
                break;
            }
        }
    }

    // 产出产品
    void produce() {
        if (outSlot == null) {
            outSlot = Globals.entityManager.Create(blueprint.outEntity.id, blueprint.outNum);
        }

        Debug.Log("produce:" + outSlot.id);

        outSlot.num += blueprint.outNum;

        Globals.entityManager.instantiateEntity(outSlot, false, gameObject.transform.position);
    }

    override public void pushEntity(Entity entity, int num) {
        Debug.Log("push:" + entity.id + "," + num);
        int index = slotMap[entity.id];
        if (index >= 0) {
            inSlot[index] = entity;
        }

        isWorking = true;
        for (int i = 0; i < inSlot.Length; i++) {
            if (inSlot[i] == null) {
                isWorking = false;
                break;
            }
        }
    }
}
