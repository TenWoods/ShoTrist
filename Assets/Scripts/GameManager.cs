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
    public GameObject normalBlock;

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

    private void Start()
    {
        int[,] map;
        BM = this.gameObject.GetComponent<BlockManager>();
        saveManager = new SaveDataSystem();
        map = ((Map)saveManager.GetData("../XBOX/Assets/Maps/test.map", typeof(Map))).map;
        BuildMap(map);
        BM.Map = map;
        BM.GameStart = true;
    }

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

    public void SaveData(string fileName, object obj)
    {
        saveDirectionary = Application.persistentDataPath + "/Save";
        string filePath = saveDirectionary + "/" + fileName + ".save";
        saveManager.CreateDirectionary(saveDirectionary);
        saveManager.SetData(filePath, obj);
    }

    public object ReadData(string fileName, Type type)
    {
        string filePath = saveDirectionary + "/" + fileName + ".save";
        return SaveManager.GetData(filePath, type);
    }
}
