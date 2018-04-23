using UnityEngine;
using System.Collections;
using System;

public class Factory : EntityBase {
    public int slotNum = 3;

    public BlueprintConfig blueprint;

    // Use this for initialization
    void Start() {
        outSlot = new Slot();
        inSlots = new Slot[slotNum];
        for (int i = 0; i < slotNum; i ++) {
            inSlots[i] = new Slot();
        }
    }

    // Update is called once per frame
    void Update() {
        if (isWorking) {
            workingTime += workSpeed * Time.deltaTime;
            if (workingTime > blueprint.time) {
                produce();
            }
        }
    }

    private void produce() {
        resetWorkingState();
        if (!isWorking) {
            return;
        }
        workingTime -= blueprint.time;
        Globals.entityManager.produceEntity(blueprint, inSlots, outSlot);
    }

    private void resetWorkingState() {
        if (blueprint == null) {
            isWorking = false;
            return;
        }

        isWorking = true;

        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            if (inSlots[i].num < blueprint.inNums[i]) {
                isWorking = false;
                break;
            }
        }
    }

    public void setBlueprint(BlueprintConfig blueprint) {
        if (this.blueprint == blueprint) {
            return;
        }

        this.blueprint = blueprint;

        //TODO 替换蓝图时，工厂内的物品应该放入背包，背包还没做，先直接销毁了
        foreach (Slot slot in inSlots) {
            if (slot.checkAvailable()) {
                Globals.entityManager.destory(slot, slot.num);
            }
        }
        if (outSlot.checkAvailable()) {
            Globals.entityManager.destory(outSlot,outSlot.num);
        }
    }

    public override bool pushEntity(GameObject obj) {
        if (blueprint == null) {
            return false;
        }

        Entity entity = Globals.entityManager.getEntityByGameObject(obj);
        bool success = false;

        int index = blueprint.getSlotIndex(entity);
        if (index >= 0) {
            inSlots[index].pushEntity(obj);
            success = true;
        }

        if (success) {
            resetWorkingState();
        }

        return success;
    }
}
