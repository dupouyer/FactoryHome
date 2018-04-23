using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour {
    public enum DIRECTION {
        LEFT = 1,
        UP = 2,
        RIGHT = 3,
        DOWN = 4
    };

    public int flag = Globals.FLAG_STATIC;

    // 输入插槽
    public Slot[] inSlots;
    // 输出插槽
    public Slot outSlot;

    // 工作速度
    public float workSpeed = 1;
    // 工作时间
    public float workingTime = 0f;
    // 工作中
    public bool isWorking;

    public float zLength = 1f;
    public float xLength = 1f;

    public DIRECTION direction {
        get {
            return (DIRECTION)Mathf.Ceil(gameObject.transform.rotation.eulerAngles.y / 90 + 1f);
        }
    }

    public void changeDirection(DIRECTION direction) {
        gameObject.transform.Rotate(new Vector3(0, ((int)direction - 1) * 90, 0));
    }

    public void changeDirection(bool isClockwise) {
        if (isClockwise) {
            gameObject.transform.Rotate(Vector3.up, 90);
        }
        else {
            gameObject.transform.Rotate(Vector3.up, -90);
        }
    }

    virtual public bool pushEntity(GameObject gameobject) {
        return false;
    }
}
