using UnityEngine;
using System.Collections;
using FairyGUI;
using System;

public class FactoryPanel : BaseArchPanel {
    GTextField title;
    GLoader archIcon;
    SlotItem outSlot;
    GList targetList;

    Factory factory;

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "FactoryPanel").asCom;

        title = contentPane.GetChild("name").asTextField;
        contentPane.GetChild("close").asButton.onClick.Add(Hide);
        archIcon = contentPane.GetChild("icon").asLoader;

        outSlot = contentPane.GetChild("out") as SlotItem;
        targetList = contentPane.GetChild("targetList").asList;
        targetList.itemRenderer = targetItemRenderer;

        outSlot.onClick.Add(handleClickOut);
    }

    BlueprintPanel subPanel;
    private void handleClickOut(EventContext context) {
        subPanel = new BlueprintPanel();
        subPanel.Show();
        subPanel.setEntity(entityBase, entity, refresh);
    }

    private void targetItemRenderer(int index, GObject item) {
        (item as SlotItem).setSlot(factory.inSlots[index]);
    }

    protected override void refresh() {
        title.text = entity.config.showName;
        archIcon.url = UIPackage.GetItemURL("Icon", entity.id);

        factory = entityBase as Factory;

        targetList.numItems = factory.inSlots.Length;
        outSlot.setSlot(factory.outSlot);

        if (factory.blueprint) {
            contentPane.GetController("c1").SetSelectedIndex(1);
        }
        else {
            contentPane.GetController("c1").SetSelectedIndex(0);
        }
    }
}
