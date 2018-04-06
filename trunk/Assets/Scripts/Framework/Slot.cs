using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot {
    public Entity entity;
    public int num;

    public void pushEntity(GameObject gameObject) {
        Entity entity = Globals.entityManager.getEntityByGameObject(gameObject);
        if (entity == null) {
            Debug.LogError("a surprised gameobject push to entity !");
            return;
        }
        Object.Destroy(gameObject);
        num += 1;
    }

    public void pushEntity(Entity entity, int num) {
        if (this.entity != null && this.entity.id != entity.id) {
            Debug.LogWarning("can't push entity to disaffinity slot。 from:" + entity.id + " to:" + this.entity.id);
            return;
        }
        this.entity = entity;
        this.num += num;
    }

    // 从一个插槽拿到另外一个插槽
    public void drawEntity(int num, Slot toSlot) {
        if (!checkAvailable()) {
            Debug.LogError("draw entity in unavilable slot");
            return;
        }

        if (toSlot.entity != null && toSlot.entity.id != entity.id) {
            Debug.LogWarning("can't draw entity to disaffinity slot。 from:" + entity.id + " to:" + toSlot.entity.id);
            return;
        }

        if (this.num < num) {
            return;
        }

        this.num -= num;

        toSlot.num += num;
        toSlot.entity = entity;
    }

    public GameObject instantiateEntity(bool isArch, Vector3 pos) {
        if (!checkAvailable()) {
            Debug.LogError("can't instantiateEntity from a empty slot");
            return null;
        }

        num -= 1;
        return Globals.entityManager.instantiateEntity(entity, isArch, pos);
    }

    public bool checkAvailable() {
        return entity != null && num > 0;
    }

    public bool checkAvailable(string id, int num) {
        return checkAvailable() && entity.id == id && this.num >= num;
    }
}
