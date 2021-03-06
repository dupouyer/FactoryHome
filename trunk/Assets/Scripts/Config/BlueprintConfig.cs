﻿using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName ="Blueprint",menuName ="Config/Blueprint",order =2 )]
public class BlueprintConfig :ScriptableObject{
    // 需要的配置
    public EntityConfig[] inEntities;
    public int[] inNums;
    // 输出的配置
    public EntityConfig outEntity;
    public int outNum = 1;
    // 生产需要的时间
    public int time;

    public int getSlotIndex(Entity entity) {
        for (int i = 0; i < inEntities.Length; i++) {
            if (inEntities[i].id == entity.id) {
                return i;
            }
        }
        return -1;
    }
}
