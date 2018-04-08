using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox: MonoBehaviour {
    // 碰撞物(不带有实体组件的 Gameobject 都是原材料）
    public List<GameObject> hits = new List<GameObject>();
    // 实体和碰撞物要区分对待
    public List<EntityBase> entities = new List<EntityBase>();

    public int length {
        get {
            int num = 0;
            entities.ForEach(e => {
                num += e.outSlot.num;
            });
            return num + hits.Count;
        }
    }

	// Use this for initialization
	void Start () {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_ENTITY) {
            EntityBase entity = other.gameObject.GetComponent<EntityBase>();
            entities.Add(entity);
        }
        else if(other.gameObject.layer == Globals.LAYER_MATERIAL){
            hits.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_ENTITY) {
            EntityBase entity = other.gameObject.GetComponent<EntityBase>();
            entities.Remove(entity);
        }
        else if(other.gameObject.layer == Globals.LAYER_MATERIAL){
            hits.Remove(other.gameObject);
        }
    }
}
