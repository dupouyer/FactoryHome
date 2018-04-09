using UnityEngine;
using System.Collections;

public class ConfigManager {
    public T getConfig<T>(string id) where T:ScriptableObject{
        T config = Resources.Load<T>("Config/" + id);
        return config;
    }

    public EntityConfig[] getEntityConfigs(EntityConfig.TYPE type) {
        EntityConfig[] configs;
        int num = 0;
        Object[] objs = Resources.LoadAll("Config/Entity/");
        foreach (EntityConfig config in objs) {
            if (config.type == type) {
                num += 1;
            }
        }

        configs = new EntityConfig[num];
        int index = 0;
        foreach (EntityConfig config in objs) {
            if (config.type == type) {
                configs[index] = config;
                index++;
            }
        }

        return configs;
    }
}
