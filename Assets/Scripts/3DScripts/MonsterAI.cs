using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class MonsterAI : MonoBehaviour
{
    [SerializeField]
    private float monsterHP; //怪物血量
    private NavMeshAgent nav;
    private GameObject player;

    public float MonsterDamage;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("player");
    }

    private void Update()
    {
        nav.SetDestination(player.transform.position);
        if (monsterHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(MonsterDamage);
        }
    }

    public void RecieveDamage(float damage)
    {
        monsterHP -= damage;
    }
}
