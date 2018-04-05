using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map
{
    public int[,] map;
}

public class MapEditor : MonoBehaviour
{
    private Map map;
    private Ray hit;
    private RaycastHit hitInfo;
    private SaveDataSystem sd;

    public InputField infield;

    private void Start()
    {
        sd = new SaveDataSystem();
        map = new Map();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SetColor(Color.black, 4);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            SetColor(Color.white, 0);
        }
    }

    private void SetColor(Color color, int colorNum)
    {
        hit = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(hit, out hitInfo))
        {
            if (hitInfo.collider.tag == "BlockButton")
            {
                hitInfo.collider.GetComponent<Renderer>().material.color = color;
                map.map[(int)(hitInfo.collider.transform.position.x / 10), (int)(hitInfo.collider.transform.position.z / 10)] = colorNum;
            }
        }
    }

    public void SaveMap()
    {
        string filePath = "../XBOX/Assets/Maps/";
        if (infield.text != string.Empty)
        {
            filePath += infield.text;
            filePath += ".map";
            sd.SetData(filePath, this.map);
        }
        else
        {
            infield.placeholder.GetComponent<Text>().text = "请输入名称!";
            return;
        }
    }
}
