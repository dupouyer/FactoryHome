using UnityEngine;
using UnityEditor;
using FairyGUI;

public class BaseArchPanel : Window {
    protected EntityBase entityBase;
    protected Entity entity;
    protected EventCallback0 hideCallback;

    public BaseArchPanel() {
        AddUISource(new UIAsset("BuildPanel"));
        modal = true;
    }

    protected override void OnShown() {
        x = (GRoot.inst.width - width) * 0.5f;
        y = (GRoot.inst.height - height) * 0.5f;
    }

    public void setEntity(EntityBase entityBase, Entity entity, EventCallback0 onHide = null) {
        this.entity = entity;
        this.entityBase = entityBase;
        this.hideCallback = onHide;
        refresh();
    }

    protected virtual void refresh() {
    }

    protected override void OnHide() {
        base.OnHide();
        if (hideCallback != null) {
            hideCallback();
        }
    }
}