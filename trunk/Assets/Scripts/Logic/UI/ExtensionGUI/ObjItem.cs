using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Common;

public class ObjItem : UI_ObjItem{

    public void SetObjData(Entity obj) {
        m_icon.url = UIPackage.GetItemURL("Icon", obj.id);
    }
}
