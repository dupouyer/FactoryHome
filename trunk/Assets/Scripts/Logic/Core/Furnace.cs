using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : EntityBase {
    BlueprintConfig blueprint;

    BlueprintConfig[] backBlueprints;

    public Slot fuelSlot;

	// Use this for initialization
	void Start () {
        inSlots = new Slot[1] {new Slot()};
        outSlot = new Slot();
        fuelSlot = new Slot();

        // 获取备选蓝图
        backBlueprints = Globals.configManager.GetBlueprintConfig(EntityConfig.TYPE.plate);
    }

	// Update is called once per frame
	void Update () {
        if (isWorking) {
            workingTime += workSpeed * Time.deltaTime;

            if (workingTime > blueprint.time) {
                produce();
                //TODO 暂时写死
                if (!inSlots[0].checkAvailable() && outSlot.checkAvailable()) {
                    resetProduceBlueprint(null);
                }
                resetWorkingState();
            }
        }
        else {
            workingTime = 0f;
        }
    }

    // 重设当前的工作状态，在输入输出有变化时调用
    public void resetWorkingState() {
        if (blueprint == null) {
            isWorking = false;
            return;
        }

        isWorking = true;

        if (!fuelSlot.checkAvailable()) {
            isWorking = false;
            return;
        }

        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            if (inSlots[i].num < blueprint.inNums[i]) {
                isWorking = false;
                break;
            }
        }
    }

    // 产出产品
    void produce() {
        resetWorkingState();
        if (!isWorking) {
            return;
        }
        workingTime -= blueprint.time;
        Globals.entityManager.produceEntity(blueprint, inSlots, outSlot);
    }

    override public bool pushEntity(GameObject obj) {
        Entity entity = Globals.entityManager.getEntityByGameObject(obj);
        bool success = false;

        if (entity.config.type == EntityConfig.TYPE.fuel) {
            if (!fuelSlot.checkAvailable() || fuelSlot.entity == entity) {
                fuelSlot.pushEntity(obj);
                success = true;
            }
        }
        // 当前有蓝图
        else if (blueprint) {
            int index = blueprint.getSlotIndex(entity);
            if (index >= 0) {
                inSlots[index].pushEntity(obj);
                success = true;
            }
        }
        else {
            foreach (BlueprintConfig backBlueprint in backBlueprints) {
                int index = backBlueprint.getSlotIndex(entity);
                if (index >= 0) {
                    resetProduceBlueprint(backBlueprint);
                    inSlots[index].pushEntity(obj);
                    success = true;
                    break;
                }
            }
        }

        if (success) {
            resetWorkingState();
        }

        return success;
    }

    void resetProduceBlueprint(BlueprintConfig blueprint) {
        this.blueprint = blueprint;
        if (blueprint) {
            outSlot.entity = Globals.entityManager.GetEntity(blueprint.outEntity.id);

            for (int i = 0; i < blueprint.inEntities.Length; i++) {
                inSlots[i].entity = Globals.entityManager.GetEntity(blueprint.inEntities[i].id);
            }
        }
        else {
            outSlot.clear();
            for (int i = 1; i < inSlots.Length; i++) {
                inSlots[i].clear();
            }
        }
    }
}
