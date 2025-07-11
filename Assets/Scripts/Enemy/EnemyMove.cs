using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMove : MonoBehaviour
{

    private NavMeshAgent agent;

    [SerializeField] Transform targetPos;

    [SerializeField] Transform playerPos;

    [SerializeField] float distance;

    [SerializeField] float stunDuration;

    [SerializeField] Transform[] PatroolPoints;

    public bool isPatrooling;

    private Animator anim;
    bool attackGirl = false;

    public bool CanAttack = true;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
       playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
       //targetPos = GameObject.FindGameObjectWithTag("TargetGirl").GetComponent<Transform>();

        EventManager.EnemyDeath += CheckIsDamageGirl;

    }

    private void OnDisable()
    {
        EventManager.EnemyDeath -= CheckIsDamageGirl;

    }

    void Update()
    {
        if (!isPatrooling)
        {
            TryAggresiveBehaviour();
        }

        else TryPatroolBehaviour();
    }

    

    void TryAggresiveBehaviour()
    {
        if ((transform.position - playerPos.position).sqrMagnitude <= distance * distance)
        {
            GoToPlayer();
            
        }

        else
        {
            GoToTarget();
            
        }
    }


    void TryPatroolBehaviour()
    {
        if ((transform.position - playerPos.position).sqrMagnitude <= distance * distance)
        {
            GoToPlayer();

        }

        else GoPatrooling();
    }

 
    public void Stun()
    {
        StartCoroutine(DelayStun());
    }

    IEnumerator DelayStun()
    {
        CanAttack = false;
        agent.enabled = false;
        anim.SetTrigger("stun");
        yield return new WaitForSeconds(stunDuration);
        agent.enabled = true;
        CanAttack = true;
        
    }

    void GoPatrooling()
    {
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            int randomPoint = Random.Range(0, PatroolPoints.Length);
            agent.SetDestination(PatroolPoints[randomPoint].position);
        }
    }

    void GoToPlayer()
    {
       if(agent && playerPos.position !=null && CanAttack) agent.SetDestination(playerPos.position);

    }

    void GoToTarget()
    {
        if(targetPos) agent.SetDestination(targetPos.position);
    }



    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "TargetGirl")
        {
            attackGirl = true;
            EventManager.CallGirlDamage();
        }

        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "TargetGirl")
        {
            EventManager.CallStopGirlDamage();
        }
    }


    void CheckIsDamageGirl()
    {
        if (attackGirl)
        {
            EventManager.CallStopGirlDamage();
        }
    }
}
