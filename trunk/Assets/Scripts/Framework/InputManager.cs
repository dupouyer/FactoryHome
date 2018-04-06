using UnityEngine;
using System.Collections;
using FairyGUI;

public class InputManager:MonoBehaviour{
    public delegate void OnClickFloorDelegate(Vector3 point);
    private event OnClickFloorDelegate onClickFloor;

    public delegate void OnUpdateDelegate();
    private event OnUpdateDelegate onUpdate;

    Plane floor;

    void Start() {
        floor = new Plane(Vector3.up, Vector3.zero);
    }

    void Update() {
        //Detect when there is a mouse click
        if (Input.GetMouseButtonUp(0) && !Stage.isTouchOnUI) {
            //Create a ray from the Mouse click position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //Initialise the enter variable
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
}
