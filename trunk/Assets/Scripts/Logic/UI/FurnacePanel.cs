using UnityEngine;
using System.Collections;
using FairyGUI;

public class FurnacePanel : BaseArchPanel {
    GTextField title;
    GLoader archIcon;
    SlotItem plateSlot;
    SlotItem oreSlot;
    SlotItem fuelSlot;

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("BuildPanel", "FurnacePanel").asCom;

        title = contentPane.GetChild("name").asTextField;
        contentPane.GetChild("close").asButton.onClick.Add(Hide);
        archIcon = contentPane.GetChild("icon").asLoader;

        plateSlot = contentPane.GetChild("plateSlot") as SlotItem;
        oreSlot = contentPane.GetChild("oreSlot") as SlotItem;
        fuelSlot = contentPane.GetChild("fuelSlot") as SlotItem;
    }

    protected override void refresh() {
        title.text = entity.config.showName;
        archIcon.url = UIPackage.GetItemURL("Icon", entity.id);

        fuelSlot.setSlot((entityBase as Furnace).fuelSlot);
        oreSlot.setSlot(entityBase.inSlots[0]);
        plateSlot.setSlot(entityBase.outSlot);
    }
}
