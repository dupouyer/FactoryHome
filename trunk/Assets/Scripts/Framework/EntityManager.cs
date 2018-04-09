using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager {
    Dictionary<string, Entity> entityList = new Dictionary<string, Entity>();

    public void CreateToSlot(string id, int num, Slot slot) {
        Debug.Log("createEntity:" + id + " num:" + num);
        Entity entity = GetEntity(id);
        entity.num += num;
        slot.pushEntity(entity, num);
    }

    public void produceEntity(BlueprintConfig blueprint, Slot[] inSlots, Slot outSlot) {
        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            if (!inSlots[i].checkAvailable(blueprint.inEntities[i].id, blueprint.inNums[i])) {
                Debug.LogError("in slot is not enough, can't produce");
                return;
            }
        }

        for (int i = 0; i < blueprint.inEntities.Length; i++) {
            destory(inSlots[i], blueprint.inNums[i]);
        }

        CreateToSlot(blueprint.outEntity.id, blueprint.outNum, outSlot);
    }

    public Entity destory(string id, int num) {
        Entity entity = GetEntity(id);
        if (entity.num < num) {
            Debug.LogError("entity num unenough can't destory, now:" + entity.num + " destory:" + num);
            return entity;
        }

        entity.num -= num;
        return entity;
    }

    public Entity destory(Slot from, int num) {
        if (from.num < num) {
            Debug.LogError("slot num not enough");
            return null;
        }
        Entity entity = destory(from.entity.id, num);
        from.num -= num;
        return entity;
    }

    public Entity GetEntity(string id) {
        Entity entity;

        if (!entityList.TryGetValue(id, out entity)) {
            EntityConfig config = Globals.configManager.getConfig<EntityConfig>("Entity/" + id);
            entity = new Entity(config);
            entityList.Add(id, entity);
        }

        return entity;
    }

    Dictionary<int, Entity> entityInstMap = new Dictionary<int, Entity>();

    public GameObject instantiateEntity(Entity entity, bool isArch, Vector3 pos, bool colliderEnable) {
        // 实例化显示对象
        GameObject gobj = Object.Instantiate(isArch && entity.config.archPrefab ? entity.config.archPrefab : entity.config.meterialPrefab);
        gobj.GetComponent<Collider>().enabled = colliderEnable;
        pos.y += entity.config.offsetY;
        gobj.transform.position = pos;
        entityInstMap.Add(gobj.GetInstanceID(), entity);

        return gobj;
    }

    public Entity getEntityByGameObject(GameObject gameObject) {
        int id = gameObject.GetInstanceID();
        return entityInstMap[id];
    }
}
