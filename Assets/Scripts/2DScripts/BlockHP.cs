using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class BlockHP : MonoBehaviour
{
    [SerializeField]
    private float blockHP;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float accelerateSpeed;
    private bool isBroken;

    public float damage;
    public float force;

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
    }

    private void Update()
    {
        if (blockHP <= 0)
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
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
            other.GetComponent<Player>().AddForce(force);
        }
    }

    public void TakeDamage(float damage)
    {
        blockHP -= damage;
    }
    
    public void SpeedDown(float downNum)
    {//TODO 延时恢复原来速度
        if (moveSpeed - downNum < BlockManager.baseSpeed) { }
        else moveSpeed -= downNum;
    }
}
