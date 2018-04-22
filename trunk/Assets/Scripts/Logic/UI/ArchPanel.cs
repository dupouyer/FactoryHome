using UnityEngine;
using System.Collections;
using FairyGUI;

public class ArchPanel:BaseArchPanel {
    GLoader archIcon;
    GTextField title;

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "Panel").asCom;

        title = contentPane.GetChild("name").asTextField;

        GButton close = contentPane.GetChild("close").asButton;
        close.onClick.Add(Hide);

        archIcon = contentPane.GetChild("icon").asLoader;
    }

    void slotItemRender(int index, GObject item) {
        SlotItem slot = item as SlotItem;
        slot.enabled = false;
        slot.setSlot(entityBase.outSlot);
    }

    override protected void refresh() {
        title.text = entity.config.showName;
        archIcon.url = UIPackage.GetItemURL("Icon", entity.config.id);
    }
}
