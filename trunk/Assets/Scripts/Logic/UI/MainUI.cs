using UnityEngine;
using System.Collections;
using FairyGUI;

public class MainUI : Window{
    GList list;

    private Slot[] slotList = new Slot[10];

    public MainUI() {
        AddUISource(new UIAsset("MainUI"));
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("Common", "ObjItem"), typeof(SlotItem));

        for (int i = 0; i < 10; i++) {
            slotList[i] = new Slot();
        }

        Globals.entityManager.CreateToSlot("coal", 10, slotList[0]);
        Globals.entityManager.CreateToSlot("copper-ore", 10, slotList[1]);
        Globals.entityManager.CreateToSlot("burner-inserter", 10, slotList[2]);
        Globals.entityManager.CreateToSlot("stone-furnace", 10, slotList[3]);
        Globals.entityManager.CreateToSlot("transport-belt", 10, slotList[4]);
    }

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("MainUI", "Hud").asCom;
        list = contentPane.GetChildAt(1).asList;
        list.itemRenderer = ItemRenderer;
        list.numItems = slotList.Length;

        Globals.input.addOnClickFloor(handlerClickFloor);
    }

    void  handlerClickFloor(Vector3 point) {
        if (list.selectedIndex >=0 && slotList[list.selectedIndex].checkAvailable()) {
            slotList[list.selectedIndex].instantiateEntity(true, point);
            ItemRenderer(list.selectedIndex, list.GetChildAt(list.selectedIndex));
        }
    }

    void ItemRenderer(int index, GObject item) {
        ((SlotItem)item).SetObjData(slotList[index]);
    }
}
