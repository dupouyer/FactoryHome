using UnityEngine;
using System.Collections;
using FairyGUI;
using System;

public class MainUI : Window{
    GList list;
    GButton rButton;
    GButton delButton;

    private Slot[] slotList = new Slot[10];

    public MainUI() {
        AddUISource(new UIAsset("MainUI"));
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("Common", "ObjItem"), typeof(SlotItem));

        for (int i = 0; i < 10; i++) {
            slotList[i] = new Slot();
        }

        Globals.entityManager.CreateToSlot("coal", 10, slotList[0]);
        Globals.entityManager.CreateToSlot("copper-ore", 10, slotList[1]);
        Globals.entityManager.CreateToSlot("burner-inserter", 100, slotList[2]);
        Globals.entityManager.CreateToSlot("stone-furnace", 100, slotList[3]);
        Globals.entityManager.CreateToSlot("transport-belt", 100, slotList[4]);
        Globals.entityManager.CreateToSlot("burner-mining-drill", 10, slotList[5]);
        Globals.entityManager.CreateToSlot("assembling-machine-1", 10, slotList[6]);
    }

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("MainUI", "Hud").asCom;

        list = contentPane.GetChildAt(1).asList;
        list.itemRenderer = ItemRenderer;
        list.numItems = slotList.Length;

        rButton= contentPane.GetChildAt(2).asButton;
        rButton.onClick.Add(onClickRotation);

        delButton = contentPane.GetChild("delBtn").asButton;
        delButton.onClick.Add(onClickDel);

        Globals.input.addOnClickFloor(handlerClickFloor);
        Globals.input.addOnClickEntity(handlerClickEntity);
        Globals.input.addOnDoubleClickEntity(handlerDoubleClickEntity);

        setDirection(Globals.entityManager.defaultDir);
    }

    private void onClickDel(EventContext context) {
        if (Globals.input.currentSelectedEntity) {
            Globals.entityManager.destory(Globals.input.currentSelectedEntity);
            Globals.input.currentSelectedEntity = null;
        }
    }

    private void handlerDoubleClickEntity(EntityBase entity) {
        BaseArchPanel panel = null;
        Entity e = Globals.entityManager.getEntityByGameObject(entity.gameObject);
        switch (e.config.panel) {
            case "Arch":
                panel = new ArchPanel();
                break;
            case "MiningDrillPanel":
                panel = new MiningDrillPanel();
                break;
            case "FurnacePanel":
                panel = new FurnacePanel();
                break;
            case "FactoryPanel":
                panel = new FactoryPanel();
                break;
        }
        if (panel != null) {
            panel.Show();
            panel.setEntity(entity, e);
        }
    }

    private void onClickRotation(EventContext context) {
        if (Globals.input.currentSelectedEntity) {
            Globals.input.currentSelectedEntity.changeDirection(true);
            handlerClickEntity(Globals.input.currentSelectedEntity);
        }
        else {
            Globals.entityManager.defaultDir =  (EntityBase.DIRECTION)((int)Globals.entityManager.defaultDir % 4 + 1);
            setDirection(Globals.entityManager.defaultDir);
        }
    }

    private void handlerClickEntity(EntityBase entity) {
        setDirection(entity.direction);
    }

    private void setDirection(EntityBase.DIRECTION dir) {
        rButton.GetController("c1").SetSelectedIndex((int)dir - 1);
    }

    void  handlerClickFloor(Vector3 point) {
        if (list.selectedIndex >=0 && slotList[list.selectedIndex].checkAvailable()) {
            slotList[list.selectedIndex].instantiateEntity(true, point);
            ItemRenderer(list.selectedIndex, list.GetChildAt(list.selectedIndex));
        }
    }

    void ItemRenderer(int index, GObject item) {
        ((SlotItem)item).setSlot(slotList[index]);
    }
}
