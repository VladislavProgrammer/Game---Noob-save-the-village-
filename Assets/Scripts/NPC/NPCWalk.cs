using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCWalk : MonoBehaviour
{

    [SerializeField] private List<Transform> _points = new List<Transform>();
    private NavMeshAgent _agent;
    public bool CanPatrolling;
    
    void OnEnable()
    {
        _agent = gameObject.GetComponent<NavMeshAgent>();
    }
    void Update()
    {
       if(CanPatrolling) TryPatrooling();
    }
    void TryPatrooling()
    
    {
        if(_agent.remainingDistance <= _agent.stoppingDistance)
        {
            int randomPoint = Random.Range(0, _points.Count);
            GoTarget(_points[randomPoint]);
        }
    }

    public void GoTarget(Transform point)
    {
        _agent.SetDestination(point.position);
    }
    
}
