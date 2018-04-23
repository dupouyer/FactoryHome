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

    public EntityConfig[] getEntityConfigs() {
        Object[] objs = Resources.LoadAll("Config/Entity/");
        EntityConfig[] outConfig = new EntityConfig[objs.Length];
        for (int i = 0; i < objs.Length; i++) {
            outConfig[i] = objs[i] as EntityConfig;
        }
        return outConfig;
    }

    public EntityConfig[] getEntityConfigs(EntityConfig.TYPE type, EntityConfig.TYPE type2) {
        EntityConfig[] configs;
        int num = 0;
        Object[] objs = Resources.LoadAll("Config/Entity/");
        foreach (EntityConfig config in objs) {
            if (config.type == type || config.type == type2) {
                num += 1;
            }
        }

        configs = new EntityConfig[num];
        int index = 0;
        foreach (EntityConfig config in objs) {
            if (config.type == type || config.type == type2) {
                configs[index] = config;
                index++;
            }
        }

        return configs;
    }

    public BlueprintConfig[] GetBlueprintConfig(EntityConfig.TYPE type) {
        BlueprintConfig[] configs;
        EntityConfig[] eConfigs = getEntityConfigs(type);
        configs = new BlueprintConfig[eConfigs.Length];
        for (int i = 0; i < eConfigs.Length; i++) {
            configs[i] = getConfig<BlueprintConfig>("Blueprint/" + eConfigs[i].id);
        }

        return configs;
    }

    public BlueprintConfig[] GetBlueprintConfig(int slotNum) {
        EntityConfig[] eConfigs = getEntityConfigs();
        BlueprintConfig[] configs = new BlueprintConfig[eConfigs.Length];

        int num = 0;
        for (int i = 0; i < eConfigs.Length; i++) {
            configs[i] = getConfig<BlueprintConfig>("Blueprint/" + eConfigs[i].id);
            if (configs[i] && configs[i].inEntities.Length == slotNum) {
                num++;
            }
        }

        BlueprintConfig[] outConfigs = new BlueprintConfig[num];
        int index = 0;
        for (int i = 0; i < eConfigs.Length; i++) {
            configs[i] = getConfig<BlueprintConfig>("Blueprint/" + eConfigs[i].id);
            if (configs[i] && configs[i].inEntities.Length == slotNum) {
                outConfigs[index] = configs[i];
                index++;
            }
        }

        return outConfigs;
    }
}
