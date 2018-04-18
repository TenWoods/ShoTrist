using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //存档管理对象
    private SaveDataSystem saveManager;
    //存档路径
    private string saveDirectionary;
    private BlockManager BM;
    private bool buildFinish;
    public GameObject normalBlock;
    public GameObject floor_1;
    public GameObject floor_2;

    public string SAVEDIR
    {
        get
        {
            return saveDirectionary;
        }
    }

    public SaveDataSystem SaveManager
    {
        get
        {
            return saveManager;
        }
    }

    public bool BuildFinish
    {
        get
        {
            return buildFinish;
        }
        set
        {
            buildFinish = value;
        }
    }

    private void Start()
    {
        buildFinish = false;
        BM = this.gameObject.GetComponent<BlockManager>();
        int[,] map;
        saveManager = new SaveDataSystem();
        saveDirectionary = Application.persistentDataPath + "/Save";
        saveManager.CreateDirectionary(saveDirectionary);
        map = ((Map)saveManager.GetData(saveDirectionary + "/floorMap.map", typeof(Map))).map;
        BuildFloor(map);
        map = ((Map)saveManager.GetData(saveDirectionary + "/test.map", typeof(Map))).map;
        BuildMap(map);
        BM.Map = map;
    }

    private void Update()
    {
        if (!BM.GameStart && buildFinish)
        {
            BM.GameStart = buildFinish;
        }
    }

    //生成地图
    private void BuildMap(int[,] map)
    {
        int i = 0, j = 0;
        for (i = 0; i < 8; i++)
        {
            for (j = 0; j < 15; j++)
            {
                if (map[i, j] == 4)
                {
                    GameObject.Instantiate(normalBlock, new Vector3(i * 10, 4.65f, j * 10), Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }

    //生成地板
    private void BuildFloor(int[,] map)
    {
        int i = 0, j = 0;
        for (i = 0; i < 8; i++)
        {
            for (j = 0; j < 15; j++)
            {
                if (map[i, j] == 4)
                {
                    GameObject.Instantiate(floor_1, new Vector3(i * 10, 0, j * 10), Quaternion.Euler(0, -45, 0));
                }
                else
                {
                    GameObject.Instantiate(floor_2, new Vector3(i * 10, 0, j * 10), Quaternion.Euler(0, -45, 0));
                }
            }
        }
    }

    //存档
    public void SaveData(string fileName, object obj)
    {
        saveDirectionary = Application.persistentDataPath + "/Save";
        string filePath = saveDirectionary + "/" + fileName + ".save";
        saveManager.CreateDirectionary(saveDirectionary);
        saveManager.SetData(filePath, obj);
    }

    //读档
    public object ReadData(string fileName, Type type)
    {
        string filePath = saveDirectionary + "/" + fileName + ".save";
        return SaveManager.GetData(filePath, type);
    }
}
