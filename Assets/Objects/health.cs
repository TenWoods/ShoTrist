using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using System.Threading;
using UnityEngine;

public class health : ObjectsManager
{
	//[SerializeField]private float liveTime;

	//[SerializeField]private GameObject Player;
	public Transform transform;
	
	public health(float livingTime) : base(livingTime)
	{//生命周期为10秒
		this.liveTime = 10f;
	}
	
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			Debug.Log("+1");
			int healing = 1;
			other.gameObject.SendMessage("AddHealth",healing);
			Destroy(this.gameObject);
		}

	}




	// Update is called once per frame
	void Update () {
		this.transform.Rotate(3,5,2);
		Destroy(this.gameObject,this.liveTime);
	}
}
