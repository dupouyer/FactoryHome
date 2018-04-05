using UnityEngine;
using System.Collections;
using FairyGUI;

public class MainUI : Window{
    GList list;

    private Entity[] objList = {
        Globals.entityManager.Create("coal"),
        Globals.entityManager.Create("copper-ore"),
        Globals.entityManager.Create("burner-inserter"),
        Globals.entityManager.Create("stone-furnace"),
        Globals.entityManager.Create("transport-belt")
    };

    public MainUI() {
        AddUISource(new UIAsset("MainUI"));
        UIObjectFactory.SetPackageItemExtension(UIPackage.GetItemURL("Common", "ObjItem"), typeof(ObjItem));
    }

    protected override void OnInit() {
        contentPane = UIPackage.CreateObject("MainUI", "Hud").asCom;
        list = contentPane.GetChildAt(1).asList;
        list.itemRenderer = ItemRenderer;
        list.numItems = objList.Length;

        Globals.input.addOnClickFloor(handlerClickFloor);
    }

    void handlerClickFloor(Vector3 point) {
        if (list.selectedIndex >=0) {
            Globals.entityManager.instantiateOneEntity(objList[list.selectedIndex], true, point);
        }
    }

    void ItemRenderer(int index, GObject item) {
        ((ObjItem)item).SetObjData(objList[index]);
    }
}
