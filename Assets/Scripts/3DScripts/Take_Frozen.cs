using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Take_Frozen : ObjectsManager
{

	public Take_Frozen(float livingTime) : base(livingTime)
	{
		this.liveTime = 15f;
	}
	
	// Use this for initialization
	void Start () {
		
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			other.gameObject.SendMessage("ChangeBullet","frozenBullet");
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		Destroy(this.gameObject,this.liveTime);
	}
}
