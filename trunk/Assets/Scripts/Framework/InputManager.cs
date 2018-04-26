using UnityEngine;
using System.Collections;
using FairyGUI;

public class InputManager:MonoBehaviour{
    public float aixLength = 100;

    public delegate void OnClickFloorDelegate(Vector3 point);
    private event OnClickFloorDelegate onClickFloor;

    public delegate void OnUpdateDelegate();
    private event OnUpdateDelegate onUpdate;

    public delegate void OnClickEntityDelegate(EntityBase entity);
    private event OnClickEntityDelegate onClickEntity;

    public delegate void OnDoubleClickDelegate(EntityBase entity);
    private event OnDoubleClickDelegate onDoubleClickEntity;

    public delegate void OnAxesDelegate(Vector3 direction);
    private event OnAxesDelegate onAxesDelegate;

    Plane floor;

    public EntityBase currentSelectedEntity;

    void Start() {
        floor = new Plane(Vector3.up, Vector3.zero);
    }

    Vector3 downP;
    Vector3 upP;
    bool isClick;
    bool isMove;

    void Update() {
        if (Stage.isTouchOnUI) {
            return;
        }

        float clickDistance = -1f;

        if (Input.GetMouseButtonDown(0)) {
            downP = Input.mousePosition;
        }

        // 处理点击
        if (Input.GetMouseButtonUp(0)) {
            upP = Input.mousePosition;
            clickDistance = Mathf.Abs(Vector3.Distance(upP, downP));
        }

        // 处理移动
        if (Input.GetMouseButton(0)) {
            if (onAxesDelegate != null) {
                float length = Mathf.Clamp(Vector3.Distance(Input.mousePosition,downP),0,aixLength);
                onAxesDelegate(Vector3.Normalize(Input.mousePosition - downP) * length / aixLength);
            }
        }

        if (clickDistance < 4 && clickDistance >= 0) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 30, (1 << Globals.LAYER_ENTITY) + (1 << Globals.LAYER_TRANSPORT))) {
                EntityBase hitEntity = hit.collider.gameObject.GetComponent<EntityBase>();
                if (hitEntity) {
                    bool isDouble = currentSelectedEntity == hitEntity;
                    currentSelectedEntity = hitEntity;

                    if (isDouble) {
                        if (onDoubleClickEntity != null) {
                            onDoubleClickEntity(currentSelectedEntity);
                        }
                    }
                    else {
                        if (onClickEntity != null) {
                            onClickEntity(currentSelectedEntity);
                        }
                    }
                }
                else {
                    currentSelectedEntity = hitEntity;
                }
            }
            else {
                currentSelectedEntity = null;

                // 没点中实体，进行地板点击检查
                float enter = 0.0f;
                if (floor.Raycast(ray, out enter)) {
                    //Get the point that is clicked
                    Vector3 hitPoint = ray.GetPoint(enter);
                    //Move your cube GameObject to the point where you clicked
                    hitPoint.x = Mathf.Ceil(hitPoint.x) - 0.5f;
                    hitPoint.y = 0;
                    hitPoint.z = Mathf.Floor(hitPoint.z) + 0.5f;

                    if (onClickFloor != null) {
                        onClickFloor(hitPoint);
                    }
                }
            }

        }
    }

    public void addOnClickFloor(OnClickFloorDelegate callback) {
        onClickFloor += callback;
    }

    public void removeonClickFloor(OnClickFloorDelegate callback) {
        onClickFloor -= callback;
    }

    public void addUpdate(OnUpdateDelegate update) {
        onUpdate += update;
    }

    public void removeUpdate(OnUpdateDelegate update) {
        onUpdate -= update;
    }

    public void addOnClickEntity(OnClickEntityDelegate callback) {
        onClickEntity += callback;
    }

    public void removeOnClickEntity(OnClickEntityDelegate callback) {
        onClickEntity -= callback;
    }

    public void addOnDoubleClickEntity(OnDoubleClickDelegate callback) {
        onDoubleClickEntity += callback;
    }

    public void removeOnDoubleClickEntity(OnDoubleClickDelegate callback) {
        onDoubleClickEntity -= callback;
    }

    public void addOnAxes(OnAxesDelegate callback) {
        onAxesDelegate += callback;
    }

    public void removeOnAxes(OnAxesDelegate callback) {
        onAxesDelegate -= callback;
    }
}
