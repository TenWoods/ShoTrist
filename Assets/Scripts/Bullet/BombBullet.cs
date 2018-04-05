using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBullet : Bullet {

	// Use this for initialization
	public BombBullet(float flySpeed, float damage, float shootSpeed) : base(flySpeed, damage, shootSpeed)
	{
	}

	public Transform transform;
	public Rigidbody rigidbody;
	private GameObject gun;

	private void Start()
	{
		gun=GameObject.FindGameObjectWithTag("3dCamera");
		Vector3 targetDirection=new Vector3(0,0,0);
		float y = gun.transform.eulerAngles.y;
		targetDirection = Quaternion.Euler(0, y-90, 0)*targetDirection;
		//Quaternion pos = new Quaternion();
		//Debug.Log(gun.transform.forward.y);
		//pos.eulerAngles=new Vector3(0,gun.transform.forward.y,gun.transform.forward.z);
		//this.transform.rotation = pos;
		this.transform.LookAt(targetDirection);
		rigidbody.velocity=gun.transform.TransformDirection(Vector3.forward)*flySpeed;
		
	}


	private void OnTriggerEnter(Collider other)
	{
		//Destroy(other);
		Destroy(gameObject);
	}
}
