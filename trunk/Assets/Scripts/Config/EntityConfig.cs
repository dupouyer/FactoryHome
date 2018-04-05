using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Config/Entity",fileName = "EntityConfig",order =1)]
public class EntityConfig : ScriptableObject {
    public string id {
        get {
            return name;
        }
    }

    public GameObject archPrefab;
    public GameObject meterialPrefab;
    public float offsetY = 0;
}
