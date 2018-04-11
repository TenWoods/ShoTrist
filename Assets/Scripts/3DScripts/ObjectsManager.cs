using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsManager : MonoBehaviour
{//做为可拾取物体的集体属性与功能继承的父类

    public float liveTime;
    public ObjectsManager(float livingTime)
    {
        this.liveTime = livingTime;

    }

    public void propertity()
    {// TODO 等待重写的功能函数
    }
    

    /*public void TimeDestory(float liveTime)
    {
        Timer += Time.deltaTime;
        //print(Timer);
        if (Timer > this.liveTime)
            Destroy(this.gameObject);
    }*/
}
