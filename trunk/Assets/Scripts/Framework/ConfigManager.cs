using UnityEngine;
using System.Collections;

public class ConfigManager {
    public T getConfig<T>(string id) where T:ScriptableObject{
        T config = Resources.Load<T>("Config/" + id);
        return config;
    }
}
