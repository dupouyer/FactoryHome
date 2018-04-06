using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using Common;

public class SlotItem : UI_ObjItem{

    public void SetObjData(Slot slot) {
        if (slot.checkAvailable()) {
            m_icon.url = UIPackage.GetItemURL("Icon", slot.entity.id);
            m_num.text = slot.num.ToString();
        }
        else {
            m_icon.url = null;
            m_num.text = "";
        }
    }
}
