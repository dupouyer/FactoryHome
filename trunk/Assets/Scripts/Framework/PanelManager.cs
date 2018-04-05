using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class PanelManager{
    List<GComponent> panels = new List<GComponent>();

    // 显示一个界面
    public GComponent ShowPanel(string packageName, string resName) {
        UIPackage.AddPackage("UI/"+packageName);
        GComponent panel = UIPackage.CreateObject(packageName, resName).asCom;
        panels.Add(panel);
        GRoot._inst.AddChild(panel);
        return panel;
    }

    // 隐藏界面
    public void HidePanel(GComponent panel) {
    }
}
