﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //存档管理对象
    private SaveDataSystem saveManager;
    //存档路径
    private string saveDirectionary;
    private BlockManager BM;
    private int[,] map;
    //游戏开始标识
    private bool gameStart = false;
    //3d玩家死亡
    private bool player_3d_dead = false;
    //2d玩家死亡
    private bool player_2d_dead = false;
    //游戏计时器
    [SerializeField]
    private float gameTime;
    public GameObject normalBlock;
    public Text TimeCounter;
    //显示获胜者
    public Text winnerText_2d;
    public Text winnerText_3d;
    //重开按钮
    public GameObject restart;
    //弹药补给和生命值补给
    public GameObject HealthBox;
    public GameObject BulletBox;

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
        BM = this.gameObject.GetComponent<BlockManager>();
        saveManager = new SaveDataSystem();
        //Debug.Log(saveDirectionary);
        map = ((Map)saveManager.GetData(Application.persistentDataPath + "/Save/test.map", typeof(Map))).map;
        BuildMap(map);
        BM.Map = map;
        gameStart = true;
    }

    private void Update()
    {
        if (gameStart)
        {
            if (gameTime >= 0 && !player_2d_dead && !player_3d_dead)
            {
                gameTime -= Time.deltaTime;
                TimeCounter.text = gameTime.ToString("F2");
            }
            else
            {
                gameStart = false;
                WinnerCheck();
                Debug.Log("GameOver");
            }
        }
    }

    //创建场景
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

    private void WinnerCheck()
    {
        restart.SetActive(true);
        if (player_3d_dead)
        {
            winnerText_2d.text = "You Win!";
            winnerText_3d.text = "You Lose!";
        }
        else
        {
            winnerText_2d.text = "You Lose!";
            winnerText_3d.text = "You Win!";
        }
    }

    public void ReStartGame()
    {
        //TODO
        SceneManager.LoadScene(1);
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

    public bool Player_2d_Dead
    {
        set
        {
            player_2d_dead = value;
        }
    }

    public bool Player_3d_Dead
    {
        set
        {
            player_3d_dead = value;
        }
    }

    public bool GameStart
    {
        get
        {
            return gameStart;
        }
    }
}
