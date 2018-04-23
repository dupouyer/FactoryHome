using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TransportByCollider : EntityBase {
    // Use this for initialization
    int myID;
    void Start() {
        myID = GetInstanceID();
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_HITBOX) {
            return;
        }

        EntityBase entity = other.gameObject.GetComponent<EntityBase>();
        if ( entity.flag == Globals.FLAG_IDLE || entity.flag == myID) {
            other.gameObject.transform.position += transform.forward * workSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_HITBOX) {
            return;
        }

        EntityBase entity = other.gameObject.GetComponent<EntityBase>();
        if (entity.flag == Globals.FLAG_IDLE) {
            entity.flag = myID;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_HITBOX) {
            return;
        }

        EntityBase entity = other.gameObject.GetComponent<EntityBase>();
        if (entity.flag == myID) {
            entity.flag = Globals.FLAG_IDLE;
        }
    }


}
