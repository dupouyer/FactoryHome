using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox: MonoBehaviour {
    // 材料
    public List<GameObject> materials = new List<GameObject>();
    // 传送带
    public TransportByCollider transport;
    // 实体
    public EntityBase entity;

    public delegate void onTriggerEnterDelegate(Collider other);
    public delegate void onTriggerExitDelegate(Collider other);
    public delegate void onTrigerEntityDelegate(EntityBase entity);
    public delegate void onTrigerTransportDelegate(TransportByCollider transport);

    public event onTriggerEnterDelegate onTriggerEnter;
    public event onTriggerExitDelegate onTriggerExit;
    public event onTrigerEntityDelegate onTriggerEntity;
    public event onTrigerTransportDelegate onTriggerTransport;

    public int length {
        get {
            int num = 0;
            if (entity) {
                num += entity.outSlot.num;
            }
            return num + materials.Count;
        }
    }

	// Use this for initialization
	void Start () {
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.isTrigger = true;
	}

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_ENTITY) {
            entity = other.gameObject.GetComponent<EntityBase>();
            if (onTriggerEntity != null) {
                onTriggerEntity(entity);
            }
        }
        else if(other.gameObject.layer == Globals.LAYER_MATERIAL){
            materials.Add(other.gameObject);
        }
        else if(other.gameObject.layer == Globals.LAYER_TRANSPORT){
            transport = other.gameObject.GetComponent<TransportByCollider>();
            if (onTriggerTransport != null) {
                onTriggerTransport(transport);
            }
        }

        if (onTriggerEnter != null) {
            onTriggerEnter(other);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.layer == Globals.LAYER_ENTITY) {
            entity = null;
            if (onTriggerEntity != null) {
                onTriggerEntity(other.GetComponent<EntityBase>());
            }
        }
        else if(other.gameObject.layer == Globals.LAYER_MATERIAL){
            materials.Remove(other.gameObject);
        }
        else if(other.gameObject.layer == Globals.LAYER_TRANSPORT){
            transport = null;
            if (onTriggerTransport != null) {
                onTriggerTransport(other.GetComponent<TransportByCollider>());
            }
        }

        if (onTriggerExit != null) {
            onTriggerExit(other);
        }
    }
}
