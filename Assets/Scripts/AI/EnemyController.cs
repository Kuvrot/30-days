using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class EnemyController : MonoBehaviour
{
    //State
    bool attacking;
    
    //Movement
    float blend = 0;
    float distance = 0; //distance between the target and the AI

    //Components
    Animator animator;
    NavMeshAgent agent;
    EnemyStats stats;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        stats = GetComponent<EnemyStats>();
        agent.speed = stats.MovementSpeed; 
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Blend" , blend);

        distance = Vector3.Distance(transform.position , GameManager.target.position);
        if (distance > agent.stoppingDistance)
        {
            agent.isStopped = false;
            agent.SetDestination(GameManager.target.position);
            blend = 1;
            attacking = false;
        }
        else
        {
            if (!attacking)
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
        StopAllCoroutines();
    }
}
