using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBase : MonoBehaviour {
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

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}
}
