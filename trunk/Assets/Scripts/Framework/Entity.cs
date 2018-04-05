using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Entity {
    public string id {
        get {
            return _config.id;
        }
    }

    // 数量
    public int num;

    // 配置
    public EntityConfig config {
        get {
            return _config;
        }
    }

    private EntityConfig _config;

    public Entity(EntityConfig config) {
        _config = config;
    }
}
