using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Map
{
    public int[,] map;

    public Map()
    {
        map = new int[8, 15];
    }
}

public class MapEditor : MonoBehaviour
{
    private Map map;
    private Ray hit;
    private RaycastHit hitInfo;
    private SaveDataSystem sd;
    private bool startGame;
    [SerializeField]
    private float mapRotateSpeed;
    [SerializeField]
    private float cameraMoveSpeed;

    public GameObject StartUI;
    public GameObject game;
    public GameObject edit;
    public GameObject camera_2D;
    public Camera camera_3D;
    public Camera editCamera;
    

    private void Start()
    {
        sd = new SaveDataSystem();
        map = new Map();
        startGame = false;
    }

    private void Update()
    {
        if (!startGame)
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
        //旋转镜头
        else
        {
            float rect_x_3D = camera_3D.rect.x;
            float rect_x = editCamera.rect.x;
            float position_y = editCamera.transform.position.y;
            if ((rect_x + 0.5f) < 0.01 || (rect_x_3D - 0.5f) < 0.01)
            {
                editCamera.rect = new Rect(-0.5f, editCamera.rect.y, editCamera.rect.width, editCamera.rect.height);
                camera_3D.rect = new Rect(0.5f, camera_3D.rect.y, camera_3D.rect.width, camera_3D.rect.height);
                edit.SetActive(false);
                camera_2D.SetActive(true);
                game.SetActive(true);
                StartUI.SetActive(false);
            }
            else
            {
                rect_x = Mathf.Lerp(rect_x, -0.5f, Time.deltaTime * cameraMoveSpeed);
                rect_x_3D = Mathf.Lerp(rect_x_3D, 0.5f, Time.deltaTime * cameraMoveSpeed);
                position_y = Mathf.Lerp(position_y, 140, Time.deltaTime * cameraMoveSpeed);
                editCamera.rect = new Rect(rect_x, editCamera.rect.y, editCamera.rect.width, editCamera.rect.height);
                camera_3D.rect = new Rect(rect_x_3D, camera_3D.rect.y, camera_3D.rect.width, camera_3D.rect.height);
                editCamera.transform.rotation = Quaternion.Lerp(editCamera.transform.rotation, Quaternion.Euler(90, 0, 0), Time.deltaTime * mapRotateSpeed);
                editCamera.transform.position = new Vector3(35, position_y, 70);
            }
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

    //保存地图
    public void SaveMap()
    {
        //文件路径
        string filePath = Application.persistentDataPath + "/Save" + "/";
        //创建文件夹
        sd.CreateDirectionary(Application.persistentDataPath + "/Save");
        filePath += "test.map";
        sd.SetData(filePath, this.map);
        startGame = true;
    }
}
