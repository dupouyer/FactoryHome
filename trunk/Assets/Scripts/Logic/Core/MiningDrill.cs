using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : EntityBase {
    public EntityConfig ore;

	// Use this for initialization
	void Start () {
        // for test
        isWorking = true;
        outSlot = new Slot();
	}

	// Update is called once per frame
	void Update () {
        if (isWorking && ore) {
            workingTime += Time.deltaTime;
            if (workingTime >= workSpeed) {
                workingTime -= workSpeed;
                produce();
            }
        }
	}

    public void setOre(EntityConfig ore) {
        this.ore = ore;
        isWorking = true;
    }

    void produce() {
        Globals.entityManager.CreateToSlot(ore.id, 1, outSlot);
        //outSlot.instantiateEntity(false, transform.position + Vector3.forward);
    }
}
