﻿using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public enum Bullets
{
    normalBullet,
    bombBullet,
    frozenBullet,
    fightBullet,
}

public class Player : MonoBehaviour
{
    public Player player;
    public float p_ForceRJ;
    public float p_ForceHover;
    //为火箭跳和悬浮分别创建变量
    public const float JumpCD=10.0f; 
    Rigidbody p_rigidbody;
	//角色
	public Transform p_transform;

	//角色控制器组件
	private CharacterController p_controller;

	//移动速度
	[SerializeField] private float p_moveSpeed = 3.0f;

	[SerializeField] private float p_jumpSpeed = 10.0f;

	//重力
	[SerializeField] private float gravity = 2.0f;

	//生命值
	public float life = 10;
    private float currentLife;

    //弹药数
    private int bulletNum;
    [SerializeField]
    private int bulletLimit;
    [SerializeField]
    private float reloadTime;

	//用于计时跳跃蓄力时间长短的计时器
	public float timer = 0;

	public float nextTime = 0.0f;
	public float fireRate = 0.5f;

	public Camera Camera_3d;

	//摄像机
	private Transform camera_Transform;

	//摄像机旋转角度
	private Vector3 camera_Rot;

	//摄像机高度
	[SerializeField] private float camera_Height = 5f;

    //2D角色动画
    [SerializeField]
    private Animator p_animator;

    bool isGround = true;

	public GameObject g_normalBullet;
	public GameObject g_bombBullet;
	public GameObject g_frozenBullet;
	public GameObject g_fightBullet;
    public Text bulletUI;
    public Slider p_HP;
    //GameManager
    public GameManager GM;

	[SerializeField]Bullets _bullets=Bullets.bombBullet;
	
	public GameObject gun;
    private bool isJumpCD=false;
	//跳跃技能是否在冷却

	// Use this for initialization
	void Start ()
	{
        p_rigidbody = GetComponent<Rigidbody>();
		p_transform = this.transform;
		//获取角色控制器组件
		p_controller = this.GetComponent<CharacterController>();

		//获取摄像机
		camera_Transform = Camera_3d.transform;
		//设置摄像机初始位置
		camera_Transform.position = p_transform.TransformPoint(0, camera_Height, 0);
		
		//设置摄像机的旋转方向和主角一致
		camera_Transform.rotation = p_transform.rotation;
		camera_Rot = camera_Transform.eulerAngles;
        //锁定鼠标
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //bullet1=new NormalBullet(1,1);
        //初始化血量
        currentLife = life;
        //初始化弹药量
        bulletNum = bulletLimit;
        bulletUI.text = bulletNum.ToString() + "/" + bulletLimit.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (GM.GameStart)
        {
            if (currentLife <= 0)
            {
                GM.Player_3d_Dead = true;
            }
            Control();
        }
    }

	void Control()
	{

        //Debug.Log("ctr");
		//获取鼠标移动距离
		float r_horizontal = Input.GetAxis("Mouse X");
		float r_vertical = Input.GetAxis("Mouse Y");

		//旋转摄像机
		camera_Rot.x -= r_vertical;
		camera_Rot.y += r_horizontal;
		camera_Transform.eulerAngles = camera_Rot;
		
		//使主角的面向方向与摄像机一致
		Vector3 camrot = camera_Transform.eulerAngles;
		camrot.x = 0;
		camrot.z = 0;
		p_transform.eulerAngles = camrot;
		
		//定义三个值控制移动
		float xm = 0, ym = 0, zm = 0;
		//重力
		//ym -= gravity * Time.deltaTime;

		//四方向移动代码
		if (Total_Input.ctr_front)
		{
            Debug.Log("ctr");
			zm += p_moveSpeed * Time.deltaTime;
		}
		else if (Total_Input.ctr_back)
		{
			zm -= p_moveSpeed * Time.deltaTime;
		}
		else if(Total_Input.ctr_left)
		{
			xm -= p_moveSpeed * Time.deltaTime;
		}
		else if (Total_Input.ctr_right)
		{
			xm += p_moveSpeed * Time.deltaTime;
		}


        if (Total_Input.ctr_jump)
        {
            if (!isJumpCD)
            {
                Debug.Log("Jump");
                p_rigidbody.AddForce(0, p_ForceRJ, 0);
                isJumpCD = true;
            }
            else Debug.Log("Can not jump!");
            
        }
        if (Total_Input.ctr_fly)
        {
            Debug.Log("fly");
            p_rigidbody.AddForce(0, p_ForceHover, 0);
        }

        CheckJumpCD();

        if (Total_Input.ctr_fire && Time.time > nextTime && bulletNum > 0)
		{
			nextTime = Time.time + fireRate;
			switch (_bullets)
			{
				case Bullets.normalBullet:
					Instantiate(g_normalBullet, gun.transform.position, gun.transform.rotation);
					break;
				case Bullets.bombBullet: 
					Instantiate(g_bombBullet, gun.transform.position, gun.transform.rotation);
					break;
				case Bullets.fightBullet:
					Instantiate(g_fightBullet, gun.transform.position, gun.transform.rotation);
					break;
				case Bullets.frozenBullet:
					Instantiate(g_frozenBullet, gun.transform.position, gun.transform.rotation);
					break;
			}
            bulletNum -= 1;
            bulletUI.text = bulletNum.ToString() + "/" + bulletLimit.ToString();
            ReloadBullet();
        }
        else if (bulletNum <= 0)
        {
            StartCoroutine(ReloadBullet());
        }
        transform.Translate(new Vector3(xm, ym, zm));
		camera_Transform.position = p_transform.TransformPoint(0, -0.6f, 0);
    }


    /// <summary>
    /// 用于检测JumpCD变量的函数，当计时器超过冷却时间时将JumpCD置false，同时重置计时器
    /// </summary>
    private void CheckJumpCD()
    {
        if(isJumpCD)
            timer += Time.deltaTime;
        if (timer > JumpCD)
        {
            isJumpCD = false;
            timer = 0;
        }
    }

    private IEnumerator ReloadBullet()
    {
        Debug.Log("Reloading");
        yield return new WaitForSeconds(reloadTime);
        bulletNum = bulletLimit;
        bulletUI.text = bulletNum.ToString() + "/" + bulletLimit.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGround = true;
    }

    //受到伤害
    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        p_HP.value = currentLife / life;
    }

    //受到冲击
    public void AddForce(float force)
    {
        p_transform.Translate(new Vector3(0, 0, -force));
    }

    /// <summary>
    /// 用于拾取生命值时添加生命
    /// </summary>
    /// <param name="healing">增加的生命值</param>
	public void AddHealth(int healing)
	{
		print("I nead healing!+1");
		this.life += healing;
	}

    /// <summary>
    /// 用于拾取子弹并更改子弹状态
    /// </summary>
    /// <param name="bulletType">子弹属性</param>
    public void GetBullet(Bullets bulletType)
    {
        this._bullets = bulletType;
        print("has changed it to " + _bullets.ToString());
    }
}
