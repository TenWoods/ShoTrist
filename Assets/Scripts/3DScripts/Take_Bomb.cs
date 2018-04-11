using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Bomb : ObjectsManager {

	public Take_Bomb(float livingTime):base(livingTime)
	{
		this.liveTime = 5f;
	}
	// Use this for initialization


	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.SendMessage("ChangeBullet","bombBullet");
			Destroy(this.gameObject);
		}
			
	}

	// Update is called once per frame
	void Update () {
		Destroy(this.gameObject,this.liveTime);
	}
}
