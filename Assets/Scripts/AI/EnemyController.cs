using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    //State
    bool attacking = false;
    bool slowDown = false;
    
    //Movement
    float blend = 0;
    float distance = 0; //distance between the target and the AI

    //Components
    Animator animator;
    NavMeshAgent agent;
    EnemyStats stats;
    //Rigidbody rigidbody;

    public GameObject[] HitBoxes;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        //rigidbody = GetComponent<Rigidbody>();
        agent.speed = stats.MovementSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        if (stats.Health <= 0)
        {
            Instantiate(GameManager.EnemyDeathPrefabs[stats.Type] , transform.position , transform.rotation);
            Destroy(gameObject);
        }

        animator.SetFloat("Blend", blend);

        distance = Vector3.Distance(transform.position , GameManager.target.position);
        if (distance > agent.stoppingDistance)
        {
            if (!slowDown)
            {
                agent.isStopped = false;
                agent.SetDestination(GameManager.target.position);
                blend = 1;
                attacking = false;
            }
        }
        else
        {
            if (!attacking && !slowDown)
            {
                blend = 0;
                agent.isStopped = true;
                animator.SetTrigger("Attack");
                attacking = true;
                StartCoroutine(WaitAttack());
            }
        }
    }

    IEnumerator WaitAttack ()
    {
        yield return new WaitForSeconds(stats.AttackRate);
        attacking = false;
        StopCoroutine(WaitAttack());
    }

    IEnumerator SlowDown()
    {
        agent.isStopped = true;
        yield return new WaitForSeconds(2);
        agent.isStopped = false;
        slowDown = false;
        StopCoroutine(SlowDown());
    }

    public void GetDamage(float damage)
    {
        stats.Health -= damage;
        animator.SetTrigger("HitReaction");
        slowDown = true;
        StartCoroutine(SlowDown());
    }

}
