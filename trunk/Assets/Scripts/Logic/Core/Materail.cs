using UnityEngine;
using System.Collections;

public class Materail : EntityBase {
    public Transport transport;

    int obstacleNum;

    private void OnTriggerEnter(Collider other) {
        EntityBase entity = other.GetComponent<EntityBase>();
        if (entity && entity.flag == Globals.FLAG_STATIC) {
            obstacleNum++;
        }

        if (obstacleNum > 0) {
            flag = Globals.FLAG_STATIC;
        }
    }

    private void OnTriggerExit(Collider other) {
        EntityBase entity = other.GetComponent<EntityBase>();
        if (entity && entity.flag == Globals.FLAG_STATIC) {
            obstacleNum--;
        }

        if (flag == Globals.FLAG_STATIC && obstacleNum <= 0) {
            flag = Globals.FLAG_IDLE;
        }
    }
}
