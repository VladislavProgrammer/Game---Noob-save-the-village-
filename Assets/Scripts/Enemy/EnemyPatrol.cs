using UnityEngine.AI;
using System.Collections.Generic;
using UnityEngine;


public class EnemyPatrol : MonoBehaviour
{
    private NavMeshAgent agent;

    public Transform playerPos;

    public float distance = 5;

    public List<Transform> points = new List<Transform>();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(points[0].position);
    }


    private void Update()
    {
        CheckState();
    }


    void CheckState()
    {
        if ((transform.position - playerPos.position).sqrMagnitude <= distance * distance)
        {
            GoToPlayer();
        }

        else
        {
            Patrol();
        }
    }

    void GoToPlayer()
    {
        agent.SetDestination(playerPos.position);

    }

    void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(points[Random.Range(0, points.Count)].position);
        }
    }
}
