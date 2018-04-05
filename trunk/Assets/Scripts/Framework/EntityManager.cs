using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager {
    public Entity Create(string id, int num = 1) {
        EntityConfig config = Globals.configManager.getConfig<EntityConfig>(id);

        Entity obj = new Entity(config);
        return obj;
    }

    public GameObject instantiateOneEntity(Entity entity, bool isArch, Vector3 pos) {
        GameObject gobj = Object.Instantiate(isArch && entity.config.archPrefab ? entity.config.archPrefab : entity.config.meterialPrefab);
        pos.y += entity.config.offsetY;
        gobj.transform.position = pos;
        return gobj;
    }
}
