using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Player : MonoBehaviour
{
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
	public int life = 10;

	//用于计时跳跃蓄力时间长短的计时器
	[SerializeField] float timer = 0;

	public float nextTime = 0.0f;
	public float fireRate = 0.5f;

	public Camera Camera_3d;

	//摄像机
	private Transform camera_Transform;

	//摄像机旋转角度
	private Vector3 camera_Rot;

	//摄像机高度
	[SerializeField] private float camera_Height = 0.1f;

	public GameObject g_normalBullet;
	public GameObject g_bombBullet;
	public GameObject g_frozenBullet;
	public GameObject g_fightBullet;

	enum Bullets
	{
		normalBullet,
		bombBullet,
	 	frozenBullet,
		fightBullet,
	}

	Bullets _bullets=Bullets.bombBullet;
	
	public GameObject gun;
	
	// Use this for initialization
	void Start ()
	{
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
		Cursor.visible = false;

//		bullet1=new NormalBullet(1,1);
	}
	
	// Update is called once per frame
	void Update () {
		if(life<=0)
			return;
		Control();
		//fire();
	}

	void Control()
	{

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
		ym -= gravity * Time.deltaTime;

		//四方向移动代码
		if (Input.GetKey(KeyCode.UpArrow))
		{
			zm += p_moveSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			zm -= p_moveSpeed * Time.deltaTime;
		}
		else if(Input.GetKey(KeyCode.LeftArrow))
		{
			xm -= p_moveSpeed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			xm += p_moveSpeed * Time.deltaTime;
		}
		
		
		//跳跃部分代码
		if (Input.GetMouseButton(1)&& p_controller.isGrounded)
		{
			
			timer += Time.deltaTime;
			if (timer >= 5.0f)
			{
				ym = timer;
				timer = 0;
			}
		}
		else if (Input.GetMouseButtonUp(1))
		{
			ym = timer;
			timer = 0;
		}

		if (Input.GetMouseButton(0) && Time.time > nextTime)
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
			
		}
		
		p_controller.Move(p_transform.TransformDirection(new Vector3(xm, ym, zm)));
		camera_Transform.position = p_transform.TransformPoint(0, -0.6f, 0);

		
	}

	/*void fire()
	{
		if (Input.GetMouseButton(0))
		{
			bullet1.Shoot(2,gun.transform.position);
		}
	}*/

	void AddHealth(int  healing)
	{
		print("I nead healing!+1");
		this.life += healing;
	}
}
