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
    public enum TYPE {
        ore = 1, //矿石
        plate = 2, // 金属板材
        material = 3, // 原材料
        mining = 4, // 矿机
        transport = 5, // 传送带
        arm = 6, // 机械臂
        furnace = 7, // 炉子
        factory = 8 // 工厂
    }

    public TYPE type;
    public string showName;
    public string panel;
    public GameObject archPrefab;
    public GameObject meterialPrefab;
    public float offsetY = 0;
}
