using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : ObjectsManager
{
    [SerializeField]
    private Bullets bulletType;

    public BulletBox(float livingTime) : base(livingTime)
    {
        liveTime = 10f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SendMessage("GetBullet", bulletType);
        }
    }
}
