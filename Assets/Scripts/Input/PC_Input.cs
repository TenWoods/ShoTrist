using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Input : Total_Input {
//检测pc平台的输入

    /// <summary>
    /// 输入控制
    /// </summary>
    [SerializeField] private KeyCode Ctr_Front = KeyCode.UpArrow;
    [SerializeField] private KeyCode Ctr_Back = KeyCode.DownArrow;
    [SerializeField] private KeyCode Ctr_Left = KeyCode.LeftArrow;
    [SerializeField] private KeyCode Ctr_Right = KeyCode.RightArrow;

    [SerializeField] private KeyCode Ctr_W = KeyCode.W;
    [SerializeField] private KeyCode Ctr_A = KeyCode.A;
    [SerializeField] private KeyCode Ctr_S = KeyCode.S;
    [SerializeField] private KeyCode Ctr_D = KeyCode.D;

    [SerializeField] private int rightMouse = 1;
    [SerializeField] private int leftMouse = 0;
    // Update is called once per frame
    void Update () {
        if (Input.GetKey(Ctr_Front)) ctr_front = true;
        else ctr_front = false;

        if (Input.GetKey(Ctr_Back)) ctr_back = true;
        else ctr_back = false;

        if (Input.GetKey(Ctr_Left)) ctr_left = true;
        else ctr_left = false;

        if (Input.GetKey(Ctr_Right)) ctr_right = true;
        else ctr_right = false;

        if (Input.GetMouseButtonDown(rightMouse)) ctr_jump = true;
        else ctr_jump = false;

        if (Input.GetMouseButton(rightMouse)) ctr_fly = true;
        else ctr_fly = false;

        if (Input.GetMouseButton(leftMouse)) ctr_fire = true;
        else ctr_fire = false;

        if (Input.GetKeyDown(Ctr_W)) ctr_w = true;
        else ctr_w = false;

        if (Input.GetKeyDown(Ctr_A)) ctr_a = true;
        else ctr_a = false;

        if (Input.GetKeyDown(Ctr_S)) ctr_s = true;
        else ctr_s = false;

        if (Input.GetKeyDown(Ctr_D)) ctr_d = true;
        else ctr_d = false;
    }
}
