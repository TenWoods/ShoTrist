using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBullet : Bullet {

	
	public NormalBullet(float flySpeed, float damage, float shootSpeed) : base(flySpeed, damage, shootSpeed){}
	public Rigidbody rigidbody;
	public GameObject tgun;
	
	void Start ()
	{
		rigidbody.velocity = tgun.transform.TransformDirection(Vector3.forward * flySpeed);
	}
	
}
