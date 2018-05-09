using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

public class BlockHP : MonoBehaviour
{
    [SerializeField]
    private float currentblockHP;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float accelerateSpeed;
    private bool isBroken;

    public float damage;
    public float force;
    public float blockHP;
    public SpriteRenderer sprite;

    public bool IsBroken
    {
        get
        {
            return isBroken;
        }
    }

    public float MS
    {
        get
        {
            return moveSpeed;
        }
    }

    public float AS
    {
        get
        {
            return accelerateSpeed;
        }
    }

    private void Start()
    {
        isBroken = false;
        currentblockHP = blockHP;
    }

    private void Update()
    {
        if (currentblockHP <= 0)
        {
            SetBlockUnsee();
        }
    }

    private void SetBlockUnsee()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        this.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
        isBroken = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !this.CompareTag("StillBlock"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            other.GetComponent<Player>().AddForce(force);
        }
    }

    public void TakeDamage(float damage)
    {
        currentblockHP -= damage;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - damage / blockHP);
        //Debug.Log(sprite.color);
    }
    
    public void SpeedDown(float downNum)
    {//TODO 延时恢复原来速度
        if (moveSpeed - downNum < BlockManager.baseSpeed) { }
        else moveSpeed -= downNum;
    }
}
