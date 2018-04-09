using UnityEngine;
using UnityEditor;
using FairyGUI;

public class BaseArchPanel : Window {
    protected EntityBase entityBase;
    protected Entity entity;
    public BaseArchPanel() {
      AddUISource(new UIAsset("BuildPanel"));
    }

    public void setEntity(EntityBase entityBase, Entity entity) {
        this.entity = entity;
        this.entityBase = entityBase;
        refresh();
    }

    protected virtual void refresh() {
    }
}