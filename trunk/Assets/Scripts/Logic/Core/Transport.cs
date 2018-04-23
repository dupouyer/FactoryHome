using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 搬运组件
public class Transport : EntityBase {
    float step = 0.1f;

    HitBox next;
    HitBox prev;

    Transform platform;
    Transform platformShow;

    Animation anim;

    float clipLength;
    int indexLenght;
    int cargoNum;

    public bool isHead = true;
    public EntityBase [] pos;
    public float[] schedule;

    void Start() {
        prev = transform.GetChild(0).GetComponent<HitBox>();
        next = transform.GetChild(1).GetComponent<HitBox>();
        //next.onTriggerTransport += handlerNextTransport;

        platform = transform.GetChild(2);
        platformShow = transform.GetChild(3);

        indexLenght = Mathf.FloorToInt(1 / step);

        pos = new EntityBase[indexLenght * 2];
        schedule = new float[indexLenght * 2];

        anim = GetComponent<Animation>();
        clipLength = anim.clip.length;
    }

    void Update () {
        workingTime += workSpeed * Time.deltaTime;

        anim[anim.clip.name].enabled = true;
        anim[anim.clip.name].weight = 1;
        anim[anim.clip.name].time =  workingTime;
        anim.Sample();
        anim[anim.clip.name].enabled = false;

        platformShow.position = platform.GetChild(0).position;

        if (!isHead) {
            return;
        }

        UpdateTransport();
	}

    public void UpdateTransport() {
        if (cargoNum > 0) {
            updateCargoPosition();
        }

        if (next.transport) {
            //next.transport.UpdateTransport();
        }
    }

    private void updateCargoPosition() {
        int index, direction;
        float time = 0;
        for (int i = 0; i < pos.Length; i++) {
            if (pos[i] == null) {
                continue;
            }

            // 索引位置
            index = i / 2;
            // 方位位置
            direction = i % 2;

            time = index * step + workingTime;

            // 时间到了，下站
            if (time >= schedule[i]) {
                popCargo(i);
            }
            else {
                // 采样动画获取具体位置
                anim[anim.clip.name].enabled = true;
                anim[anim.clip.name].weight = 1;
                anim[anim.clip.name].time = time;
                anim.Sample();
                anim[anim.clip.name].enabled = false;

                // 更新货物的位置
                pos[i].transform.position = platform.GetChild(direction).position;
                pos[i].transform.rotation = platform.GetChild(direction).rotation;
            }
        }
    }

    // 坐标是一个世界坐标
    public bool pushCargo(EntityBase cargo, Vector3 position) {
        Vector3 local = transform.InverseTransformPoint(position);
        int direction = local.x > 0 ? 1 : 0;

        float normalzeWorkingTime = workingTime / clipLength;
        float startWorkingTime = Mathf.Floor(normalzeWorkingTime);
        float endWorkingTime = Mathf.Ceil(normalzeWorkingTime);

        float normalizeZ = Mathf.Clamp((local.z + zLength * 0.5f) / zLength, step , 1);

        int index;
        // 当前时间的左还是右
        bool isLeft = normalizeZ > (normalzeWorkingTime - startWorkingTime);

        if (isLeft) {
            index = Mathf.FloorToInt((normalizeZ + startWorkingTime - normalzeWorkingTime) / step);
        }
        else {
            index = Mathf.FloorToInt((endWorkingTime - normalzeWorkingTime + normalizeZ) / step);
        }
        int uindex = index * 2 + direction;

        if (index < 0 || index >= indexLenght) {
            uindex = index * 2 + direction;
        }

        if (pos[uindex] == null) {
            pos[uindex] = cargo;
            // 获取下一个完成时刻
            if (isLeft) {
                schedule[uindex] = (endWorkingTime - normalizeZ) * clipLength;
            }
            else {
                schedule[uindex] = (endWorkingTime + 1 - normalizeZ) * clipLength;
            }

            if (cargo is Materail) {
                (cargo as Materail).transport = this;
            }

            cargoNum ++;
            updateCargoPosition();
            return true;
        }
        else {
            return false;
        }
    }

    public void popCargo(int uindex) {
        if (prev.transport) {
            //prev.transport.pushCargo(pos[uindex], pos[uindex].transform.position);
        }
        else {
            if (pos[uindex]is Materail) {
                (pos[uindex] as Materail).transport = null;
            }
        }
        pos[uindex] = null;
        cargoNum--;
    }

    public void drawCargo(EntityBase cargo) {
        for (int i = 0; i < pos.Length; i++) {
            if (pos[i] == cargo) {
                if (pos[i]is Materail) {
                    (pos[i] as Materail).transport = null;
                }
                pos[i] = null;
                cargoNum--;
            }
        }
    }

    private void handlerNextTransport(Transport transport) {
        if (next.transport) {
            //next.transport.isHead = false;
        }
        else {
            transport.isHead = true;
        }
    }

    private void OnDestroy() {
        if (next.transport) {
            //next.transport.isHead = true;
        }
    }
}
