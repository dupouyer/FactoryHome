using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager {
    public Entity Create(string id, int num = 1) {
        EntityConfig config = Globals.configManager.getConfig<EntityConfig>(id);

        Entity obj = new Entity(config);
        return obj;
    }

    Dictionary<int, Entity> entityInstMap = new Dictionary<int, Entity>();

    public GameObject instantiateEntity(Entity entity, bool isArch, Vector3 pos) {
        entity.num -= 1;

        // 实例化显示对象
        GameObject gobj = Object.Instantiate(isArch && entity.config.archPrefab ? entity.config.archPrefab : entity.config.meterialPrefab);
        pos.y += entity.config.offsetY;
        gobj.transform.position = pos;
        entityInstMap.Add(gobj.GetInstanceID(), entity);

        return gobj;
    }

    // 还原物体的实例
    public void restoreEntityInstance(GameObject gameObject) {
        Entity entity = entityInstMap[gameObject.GetInstanceID()];
        entity.num += 1;
        GameObject.Destroy(gameObject);
    }

    public Entity getEntityByGameObject(GameObject gameObject) {
        int id = gameObject.GetInstanceID();
        return entityInstMap[id];
    }
}
