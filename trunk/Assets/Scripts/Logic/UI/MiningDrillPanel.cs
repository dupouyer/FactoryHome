using UnityEngine;
using System.Collections;
using FairyGUI;
using System;

public class MiningDrillPanel : BaseArchPanel {
    GTextField title;
    GLoader archIcon;
    SlotItem outSlot;
    GList targetList;

    EntityConfig[] oreList;

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "MiningDrillPanel").asCom;

        title = contentPane.GetChild("name").asTextField;
        contentPane.GetChild("close").asButton.onClick.Add(Hide);
        archIcon = contentPane.GetChild("icon").asLoader;
        outSlot = contentPane.GetChild("out") as SlotItem;
        targetList = contentPane.GetChild("targetList").asList;
        targetList.itemRenderer = targetItemRenderer;
        targetList.onClickItem.Add(onClickTarget);
    }

    private void onClickTarget(EventContext context) {
        EntityConfig ore = oreList[targetList.selectedIndex];
        entityBase.gameObject.GetComponent<MiningDrill>().ore = ore;
        entityBase.outSlot.entity = Globals.entityManager.GetEntity(ore.id);
        contentPane.GetController("c1").SetSelectedIndex(0);
    }

    private void targetItemRenderer(int index, GObject item) {
        (item as SlotItem).setConfig(oreList[index]);
    }

    protected override void refresh() {
        title.text = entity.config.showName;
        archIcon.url = UIPackage.GetItemURL("Icon", entity.config.id);

        outSlot.setSlot(entityBase.outSlot);
        contentPane.GetController("c1").SetSelectedIndex(entityBase.outSlot.entity == null ? 1 : 0);

        oreList = Globals.configManager.getEntityConfigs(EntityConfig.TYPE.ore);
        targetList.numItems = oreList.Length;
    }
}
