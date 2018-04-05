using UnityEngine;
using UnityEditor;
using FairyGUI;

public class UIAsset : IUISource {
    public UIAsset(string packageName) {
        _fileName = packageName;
    }

    private string _fileName;
    private bool _loaded;

    public string fileName {
        get {
            return _fileName;
        }

        set {
            _fileName = value;
        }
    }

    public bool loaded {
        get {
            return _loaded;
        }
    }

    public void Load(UILoadCallback callback) {
        UIPackage.AddPackage("UI/" + _fileName);
        _loaded = true;
        callback.Invoke();
    }
}