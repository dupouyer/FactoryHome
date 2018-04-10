using UnityEngine;
using System.Collections;
using FairyGUI;
using System;

public class MainUI : Window{
    GList list;
    GButton rButton;
    GButton leftButton;
    GButton rightButton;
    GButton upButton;
    GButton downButton;

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
        Globals.entityManager.CreateToSlot("burner-mining-drill", 10, slotList[5]);
    }

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("MainUI", "Hud").asCom;

        list = contentPane.GetChildAt(1).asList;
        list.itemRenderer = ItemRenderer;
        list.numItems = slotList.Length;

        rButton= contentPane.GetChildAt(2).asButton;
        rButton.onClick.Add(onClickRotation);

        leftButton = contentPane.GetChildAt(3).asButton;
        leftButton.onClick.Add(clickLeft);
        rightButton = contentPane.GetChildAt(4).asButton;
        rightButton.onClick.Add(clickRight);
        upButton = contentPane.GetChildAt(5).asButton;
        upButton.onClick.Add(clickUp);
        downButton = contentPane.GetChildAt(6).asButton;
        downButton.onClick.Add(clickDown);

        Globals.input.addOnClickFloor(handlerClickFloor);
        Globals.input.addOnClickEntity(handlerClickEntity);
        Globals.input.addOnDoubleClickEntity(handlerDoubleClickEntity);
    }

    void clickUp() {
        Camera.main.transform.position += Vector3.right;
    }

    void clickDown() {
        Camera.main.transform.position += Vector3.left;
    }

    void clickLeft() {
        Camera.main.transform.position += Vector3.forward;
    }

    void clickRight() {
        Camera.main.transform.position += Vector3.back;
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
    }

    private void handlerClickEntity(EntityBase entity) {
        rButton.GetController("c1").SetSelectedIndex((int)entity.direction - 1);
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
