using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using System;

public class GameStart : MonoBehaviour {
    public float CameraSpeed = 10f;

    private void Awake() {
        UIPackage.AddPackage("UI/Common");
        UIPackage.AddPackage("UI/Icon");
        StageCamera.CheckMainCamera();
        Globals.input = gameObject.AddComponent<InputManager>();
    }

    Vector3 cameraPos;

    // Use this for initialization
    void Start () {
        // for test
        GRoot.inst.ShowWindow(new MainUI());
        Globals.input.addOnMove(handleOnMove);
        cameraPos = Camera.main.transform.position;
    }

    private void handleOnMove(Vector3 direction) {
        direction *= CameraSpeed * Time.deltaTime;
        cameraPos += new Vector3(-direction.y, direction.z , direction.x);
    }

    // Update is called once per frame
    void Update () {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraPos, 0.5f);
	}
}
