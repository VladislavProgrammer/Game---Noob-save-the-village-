using System;
using UnityEngine;
using UnityEngine.AI;

public class AICharacter : MonoBehaviour
{
    public NavMeshAgent agent;
    protected Animator animator;
    protected Transform currentTarget;

    [SerializeField] public float detectionRadius = 50f;
    [SerializeField] protected LayerMask enemyLayer;
    
    [SerializeField] public Transform[] waypoints;

    private IAIState currentState;

    [SerializeField] BasicSword sword;
    private void OnEnable()
    {
        EventManager.WaweCompleted += SetPatrolState;
    }

    private void OnDisable()
    {
        EventManager.WaweCompleted -= SetPatrolState;
    }

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetPatrolState(); // по умолчанию солдаты патрулируют 
    }

    void SetPatrolState()
    {
        ChangeState(new PatrolState(this, waypoints));
    }
    
    public void ChangeState(IAIState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState?.Enter();
    }

    private void Update()
    {
        if(currentState!=null) currentState.Update();
        Debug.Log(agent.stoppingDistance);
    }

    public bool HasReachedDestination()
    {
        return !agent.pathPending 
               &&  agent.remainingDistance <= agent.stoppingDistance
               &&  (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        
    }

    public Transform FindNearestEnemy()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        if (hits.Length == 0) return null;

        Transform nearestEnemy = null;
        float minDistance = float.MaxValue;
            
        foreach (var hit in hits)
        {
            float distance = Vector3.Distance(transform.position, hit.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestEnemy = hit.transform;
            }  
        }

        return nearestEnemy;

    }
    
   
    public void Attack()
    {
        animator.SetTrigger("attack");
        sword.CanDamage = true;
        Invoke("NoneAttack", 1f);
        Debug.Log("Атакуем врага");
    }

    void NoneAttack()
    {
        sword.CanDamage = false;
    }
  
}
