using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float flySpeed;
    public float damage;

    public float shootSpeed;


    public Bullet(float flySpeed,float damage,float shootSpeed)
    {
        this.damage = damage;
        this.flySpeed = flySpeed;
        this.shootSpeed = shootSpeed;
    }


}
