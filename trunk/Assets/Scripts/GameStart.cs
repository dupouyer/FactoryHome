using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class GameStart : MonoBehaviour {
    private void Awake() {
        UIPackage.AddPackage("UI/Common");
        UIPackage.AddPackage("UI/Icon");
        StageCamera.CheckMainCamera();
        Globals.input = gameObject.AddComponent<InputManager>();
    }

    // Use this for initialization
    void Start () {
        // for test
        GRoot.inst.ShowWindow(new MainUI());
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
