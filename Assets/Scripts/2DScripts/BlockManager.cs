using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField]
    private float MoveSpeed;
    [SerializeField]
    private float AccelerateSpeed;
    //地图数组：红为1，蓝为2，黄为3，无方块为0, 墙为4
    private int[,] map;
    //静止方块数组
    private GameObject[,] Blocks;
    [SerializeField]
    private bool initNewBlock;
    [SerializeField]
    private bool initAmonster;
    public GameManager GM;
    private GameObject Block_1;
    private GameObject Block_2;
    public GameObject baseBlock;
    public GameObject monster;
    public Sprite[] sprites;
    public Transform spawnPoint;
    public Transform monsterPoint;
    public static float baseSpeed = 5;

    public int[,] Map
    {
        get
        {
            return map;
        }
        set
        {
            map = value;
        }
    }

    private void Awake()
    {
        initNewBlock = false;
        initAmonster = false;
        map = new int[8, 15];
        Blocks = new GameObject[8, 15];
    }

    private void Update()
    {
        if (GM.GameStart)
        {
            if (!initNewBlock)
            {
                InitBlock();
            }
            else
            {
                if (Block_1.GetComponent<BlockHP>().IsBroken && Block_2.GetComponent<BlockHP>().IsBroken)
                {
                    initNewBlock = false;
                    DestroyBlock(Block_1);
                    DestroyBlock(Block_2);
                }
                else
                {
                    BlockMove();
                }
            }
        }
    }

    public void InitBlock()
    {
        GameObject.Instantiate(baseBlock, spawnPoint.position, spawnPoint.rotation);
        Block_1 = GameObject.FindWithTag("Block_1");
        Block_2 = GameObject.FindWithTag("Block_2");
        RandomColor(Block_1);
        RandomColor(Block_2);
        initNewBlock = true;
    }

    private void DestroyBlock(GameObject block)
    {
        Destroy(block);
    }

    //随机生成颜色
    private void RandomColor(GameObject paint)
    {
        int color;
        color = (int)Random.Range(1, 4);
        switch (color)
        {
            case 1:
                paint.GetComponent<Renderer>().material.color = Color.red;
                paint.GetComponentInChildren<SpriteRenderer>().sprite = sprites[0];
                break;
            case 2:
                paint.GetComponent<Renderer>().material.color = Color.blue;
                paint.GetComponentInChildren<SpriteRenderer>().sprite = sprites[1];
                break;
            case 3:
                paint.GetComponent<Renderer>().material.color = Color.yellow;
                paint.GetComponentInChildren<SpriteRenderer>().sprite = sprites[2];
                break;
        }
    }

    //控制方块移动
    private void BlockMove()
    {
        Vector3 block_1 = Block_1.transform.position;
        Vector3 block_2 = Block_2.transform.position;
        float rotateFlag_1 = 0;
        float rotateFlag_2 = 0;
        bool stopFlag = false;
        map[(int)(block_1.x / 10), (int)(block_1.z / 10)] = 0;
        map[(int)(block_2.x / 10), (int)(block_2.z / 10)] = 0;
        //向左移动
        if (Input.GetKeyDown(KeyCode.A))
        {
            if ((block_1.x - 10) / 10 >= 0 && (block_2.x - 10) / 10 >= 0)
            {
                if (map[(int)(block_1.x - 10) / 10, (int)(block_1.z) / 10] == 0 && map[(int)(block_2.x - 10) / 10, (int)(block_2.z) / 10] == 0)
                {
                    block_1 += new Vector3(-10, 0, 0);
                    block_2 += new Vector3(-10, 0, 0);
                }
            }
        }
        //向右移动
        if (Input.GetKeyDown(KeyCode.D))
        {
            if ((block_1.x + 10) / 10 < 8 && (block_2.x + 10) / 10 < 8)
            {
                if (map[(int)(block_1.x + 10) / 10, (int)(block_1.z) / 10] == 0 && map[(int)(block_2.x + 10) / 10, (int)(block_2.z) / 10] == 0)
                {
                    block_1 += new Vector3(10, 0, 0);
                    block_2 += new Vector3(10, 0, 0);
                }
            }
        }
        //旋转
        if (Input.GetKeyDown(KeyCode.W))
        {
            rotateFlag_1 = (block_1.z * 10 + block_1.x);
            rotateFlag_2 = (block_2.z * 10 + block_2.x);
            //左边的方块向上
            if ((int)Mathf.Abs(rotateFlag_1 - rotateFlag_2) < 90)
            {
                if (rotateFlag_1 > rotateFlag_2)
                {
                    if ((block_1.z + 15) / 10 < 15)
                    {
                        if (map[(int)(block_1 + new Vector3(0, 0, 10)).x / 10, (int)(block_1 + new Vector3(0, 0, 10)).z / 10] == 0)
                        {
                            block_2 = block_1 + new Vector3(0, 0, 10);
                        }
                    }
                }
                else
                {
                    if ((block_2.z + 15) / 10 < 15)
                    {
                        if (map[(int)(block_2 + new Vector3(0, 0, 10)).x / 10, (int)(block_2 + new Vector3(0, 0, 10)).z / 10] == 0)
                        {
                            block_1 = block_2 + new Vector3(0, 0, 10);
                        }
                    }
                }
            }
            //上边的方块向下
            else
            {
                if (rotateFlag_1 < rotateFlag_2)
                {
                    if ((block_1.x + 15) / 10 <= 8)
                    {
                        if (map[(int)(block_1 + new Vector3(10, 0, 0)).x / 10, (int)(block_1 + new Vector3(10, 0, 0)).z / 10] == 0)
                        {
                            block_2 = block_1 + new Vector3(10, 0, 0);
                        }
                    }
                }
                else
                {
                    if ((block_2.x + 15) / 10 <= 8)
                    {
                        if (map[(int)(block_1 + new Vector3(10, 0, 0)).x / 10, (int)(block_1 + new Vector3(10, 0, 0)).z / 10] == 0)
                        {
                            block_1 = block_2 + new Vector3(10, 0, 0);
                        }
                    }
                }
            }
        }
        //向下移动
        if (Input.GetKey(KeyCode.S))
        {
            AccelerateSpeed = Mathf.Min(Block_1.GetComponent<BlockHP>().AS, Block_2.GetComponent<BlockHP>().AS);
            block_1 += new Vector3(0, 0, -AccelerateSpeed * Time.deltaTime);
            block_2 += new Vector3(0, 0, -AccelerateSpeed * Time.deltaTime);
        }
        else
        {
            MoveSpeed = Mathf.Min(Block_1.GetComponent<BlockHP>().MS, Block_2.GetComponent<BlockHP>().MS);
            block_1 += new Vector3(0, 0, -MoveSpeed * Time.deltaTime);
            block_2 += new Vector3(0, 0, -MoveSpeed * Time.deltaTime);
        }
        //碰撞处理
        if (map[(int)((block_1.x) / 10), (int)((block_1.z) / 10)] == 0 && map[(int)((block_2.x) / 10), (int)((block_2.z) / 10)] == 0 && block_1.z / 10 >= 0 && block_2.z / 10 >= 0 && !stopFlag)
        {
            Block_1.transform.position = block_1;
            Block_2.transform.position = block_2;
        }
        else if (map[(int)((block_1.x) / 10), (int)((block_1.z) / 10)] != 0 || map[(int)((block_2.x) / 10), (int)((block_2.z) / 10)] != 0 || block_1.z / 10 < 0 || block_2.z / 10 < 0 || stopFlag)
        {
            if (!Block_1.GetComponent<BlockHP>().IsBroken)
            {
                SetBlockInMap(Block_1);
                CheckDestroy((int)((Block_1.transform.position.x) / 10), (int)((Block_1.transform.position.z) / 10));
                Block_1.tag = "StillBlock";
            }
            else
            {
                DestroyBlock(Block_1);
                map[(int)((Block_1.transform.position.x) / 10), (int)((Block_1.transform.position.z) / 10)] = 0;
            }
            if (!Block_2.GetComponent<BlockHP>().IsBroken)
            {
                SetBlockInMap(Block_2);
                CheckDestroy((int)((Block_2.transform.position.x) / 10), (int)((Block_2.transform.position.z) / 10));
                Block_2.tag = "StillBlock";
            }
            else
            {
                DestroyBlock(Block_2);
                map[(int)((Block_2.transform.position.x) / 10), (int)((Block_2.transform.position.z) / 10)] = 0;
            }
            initNewBlock = false;
        }
    }

    //根据颜色设置数字
    private void SetBlockInMap(GameObject Block)
    {
        Debug.Log((int)((Block.transform.position.z) / 10));
        if (((int)((Block.transform.position.x) / 10) == 3 || ((int)((Block.transform.position.x) / 10) == 4)) && (int)((Block.transform.position.z) / 10) == 14)
        {
            GM.Player_2d_Dead = true;
            //Debug.Log((int)((Block.transform.position.z) / 10));
        }
        if (Block.GetComponent<Renderer>().material.color == Color.red)
        {
            map[(int)((Block.transform.position.x) / 10), (int)((Block.transform.position.z) / 10)] = 1;
        }
        else if (Block.GetComponent<Renderer>().material.color == Color.blue)
        {
            map[(int)((Block.transform.position.x) / 10), (int)((Block.transform.position.z) / 10)] = 2;
        }
        else
        {
            map[(int)((Block.transform.position.x) / 10), (int)((Block.transform.position.z) / 10)] = 3;
        }
        Block.transform.position = new Vector3((int)(Block.transform.position.x), 5, (int)(Block.transform.position.z));
        Blocks[(int)((Block.transform.position.x) / 10), (int)((Block.transform.position.z) / 10)] = Block;
    }

    //检测消除
    private void CheckDestroy(int x, int z)
    {
        Color check = new Color();
        int checkNum = 0;
        check = Blocks[x, z].GetComponent<Renderer>().material.color;
        //还原颜色编号
        if (check == Color.red)
            checkNum = 1;
        else if (check == Color.blue)
            checkNum = 2;
        else
            checkNum = 3;
        //横向消除的3种情况
        if ((x + 2) < 8 && (x + 1) < 8)
        {
            if (map[x + 1, z] == checkNum && map[x + 2, z] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x + 1, z]);
                Destroy(Blocks[x + 2, z]);
                map[x, z] = 0;
                map[x + 1, z] = 0;
                map[x + 2, z] = 0;
                initAmonster = true;
            }
        }
        if ((x + 1) < 8 && (x - 1) >= 0)
        {
            if (map[x + 1, z] == checkNum && map[x - 1, z] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x + 1, z]);
                Destroy(Blocks[x - 1, z]);
                map[x, z] = 0;
                map[x + 1, z] = 0;
                map[x - 1, z] = 0;
                initAmonster = true;
            }
        }
        if ((x - 2) >= 0 && (x - 1) >= 0)
        {
            if (map[x - 1, z] == checkNum && map[x - 2, z] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x - 1, z]);
                Destroy(Blocks[x - 2, z]);
                map[x, z] = 0;
                map[x - 1, z] = 0;
                map[x - 2, z] = 0;
                initAmonster = true;
            }
        }
        //竖向消除的3种情况
        if ((z + 1) < 15 && (z + 2) < 15)
        {
            if (map[x, z + 1] == checkNum && map[x, z + 2] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x, z + 1]);
                Destroy(Blocks[x, z + 2]);
                map[x, z] = 0;
                map[x, z + 1] = 0;
                map[x, z + 2] = 0;
                initAmonster = true;
            }
        }
        if ((z + 1) < 15 && (z - 1) >= 0)
        {
            if (map[x, z + 1] == checkNum && map[x, z - 1] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x, z + 1]);
                Destroy(Blocks[x, z - 1]);
                map[x, z] = 0;
                map[x, z - 1] = 0;
                map[x, z + 1] = 0;
                initAmonster = true;
            }
        }
        if ((z - 1) >= 0 && (z - 2) >= 0)
        {
            if (map[x, z - 2] == checkNum && map[x, z - 1] == checkNum)
            {
                Destroy(Blocks[x, z]);
                Destroy(Blocks[x, z - 2]);
                Destroy(Blocks[x, z - 1]);
                map[x, z] = 0;
                map[x, z - 1] = 0;
                map[x, z - 2] = 0;
                initAmonster = true;
            }
        }
        if (initAmonster)
        {
            initAmonster = false;
            GameObject.Instantiate(monster, monsterPoint.position, monsterPoint.rotation);
        }
    }
}