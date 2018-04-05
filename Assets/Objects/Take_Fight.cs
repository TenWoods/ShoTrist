using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Fight : ObjectsManager {

	public Take_Fight(float livingTime) : base(livingTime)
	{
		this.liveTime = 7f;
	}


	private void Start()
	{
		Destroy(this.gameObject,this.liveTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.SendMessage("ChangeBullet","fightBullet");
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
