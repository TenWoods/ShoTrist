using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrozenBullet : Bullet {

    float liveTime=5.0f;
    [SerializeField]float downSpeed = 5.0f;
    public FrozenBullet(float flySpeed,float damage,float shootSpeed):base(flySpeed, damage, shootSpeed)
    {}
    public Rigidbody rigidbody;
    private GameObject gun;
	// Use this for initialization
	void Start () {
        gun = GameObject.FindGameObjectWithTag("3dCamera");
        rigidbody.velocity = gun.transform.TransformDirection(Vector3.forward) * flySpeed ;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block_1") || other.gameObject.CompareTag("Block_2"))
        {
            other.gameObject.SendMessage("SpeedDown", downSpeed);
            other.gameObject.SendMessage("TakeDamage", damage);
        }
        if (!other.gameObject.CompareTag("Player"))
        {//通过取消对Player的碰撞来取消相机方向向上时的迷之碰撞
            Destroy(this.gameObject);
        }

    }
}
