﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounBullet : Bullet {
    float liveTime = 10f;

    public BounBullet(float flySpeed, float damage, float shootSpeed) : base(flySpeed, damage, shootSpeed)
    {
    }
    public Transform transform;
    public Rigidbody rigidbody;
    private GameObject gun;

    private void Start()
    {
        gun = GameObject.FindGameObjectWithTag("3dCamera");
        rigidbody.velocity = gun.transform.TransformDirection(Vector3.forward) * flySpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Block_1") || other.gameObject.CompareTag("Block_2"))
        {
            other.gameObject.SendMessage("TakeDamage", damage);
        }
        if (!other.gameObject.CompareTag("Player"))
        {//通过取消对Player的碰撞来取消相机方向向上时的迷之碰撞
            Destroy(this.gameObject);
        }
    }
}
