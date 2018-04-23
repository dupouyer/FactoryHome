using UnityEngine;
using System.Collections;
using FairyGUI;
using System;

public class BlueprintPanel : BaseArchPanel{
    GTextField title;
    GList list;

    BlueprintConfig[] blueprintConfigs;

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "BlueprintPanel").asCom;

        title = contentPane.GetChild("name").asTextField;
        contentPane.GetChild("close").asButton.onClick.Add(Hide);

        list = contentPane.GetChild("targetList").asList;
        list.itemRenderer = itemRenderer;
        list.onClickItem.Add(onClickBlurprint);

        title.text = "选择蓝图";
    }

    private void onClickBlurprint(EventContext context) {
        BlueprintConfig blueprint = blueprintConfigs[list.selectedIndex];
        (entityBase as Factory).setBlueprint(blueprint);
        Hide();
    }

    private void itemRenderer(int index, GObject item) {
        (item as SlotItem).setConfig(blueprintConfigs[index].outEntity);
    }

    protected override void refresh() {
        Factory factory = entityBase as Factory;
        blueprintConfigs = Globals.configManager.GetBlueprintConfig(factory.slotNum);
        list.numItems = blueprintConfigs.Length;
    }
}
