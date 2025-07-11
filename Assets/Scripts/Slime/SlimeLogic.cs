using UnityEngine;
using UnityEngine.AI;

public class SlimeLogic : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Sword")
        { 
            if(other.gameObject.GetComponent<AttackSword>().canAttack)
            EventManager.CallBossTakeDamage();
        }
    }

    private void Update()
    {
        TryMoveToPlayer();
    }

    void TryMoveToPlayer()
    {
        agent.SetDestination(player.position);
    }

}
