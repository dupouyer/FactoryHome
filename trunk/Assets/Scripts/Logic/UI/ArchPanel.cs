using UnityEngine;
using System.Collections;
using FairyGUI;

public class ArchPanel:Window {
    public ArchPanel() {
        AddUISource(new UIAsset("BuildPanel"));
    }

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "Panel").asCom;
        GObject g = contentPane.GetChildAt(3);
        g.asButton.onClick.Add(Callback);

        //BuildPanel.UI_Panel panel = (BuildPanel.UI_Panel)contentPane;
        //panel.m_n3.onClick.Add(Callback);
    }
    void Callback() {
        GRoot.inst.HideWindow(this);
    }
}
