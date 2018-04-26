using UnityEngine;
using System.Collections;

public class Materail : EntityBase {
    public Transport transport;

    public EntityBase obstacle;

    private void OnTriggerEnter(Collider other) {
        EntityBase entity = other.GetComponent<EntityBase>();
        if (entity && entity.flag == Globals.FLAG_STATIC) {
            obstacle = entity;
            flag = Globals.FLAG_STATIC;
        }
    }

    private void OnTriggerExit(Collider other) {
        EntityBase entity = other.GetComponent<EntityBase>();
        if (entity == obstacle) {
            flag = Globals.FLAG_IDLE;
        }
    }

    //private void Update() {
    //    // 关注障碍的状态，这里不应该用 update 观察的，比较费，应该使用 callback，以后再改
    //    if (obstacle != null && obstacle.flag != Globals.FLAG_STATIC) {
    //        obstacle = null;
    //        flag = Globals.FLAG_IDLE;
    //    }
    //}

}
