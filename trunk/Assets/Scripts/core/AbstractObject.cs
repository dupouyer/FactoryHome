using UnityEngine;
using System.Collections;

/**
* 物件的抽象类
* 物件有状态, 有世界坐标

*/
public class AbstractObject : MonoBehaviour {
    // 物体的基本状态
    public enum ObjectState {
        RES = 1, // 资源状态
        ARCH = 2 // 建筑状态
    }

    // 当前的状态
    public ObjectState state = ObjectState.RES;
}
