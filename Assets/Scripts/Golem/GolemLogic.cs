using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemLogic : MonoBehaviour
{

    [SerializeField] Transform playerPos;
    [SerializeField] float distance;
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] private AudioSource _armorSound;
    public bool canWalk = true;
    public int delay = 3;



    private void Update()
    {
        CheckState();
    }


    void CheckState()
    {
        if ((transform.position - playerPos.position).sqrMagnitude <= distance * distance)
        {
            Attack();
            
        }

        else
        {
            GoToPlayer();

        }
    }



    void GoToPlayer()
    {
        if (canWalk)
        {
            agent.SetDestination(playerPos.position);

        }
        

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            _armorSound.Play();
            Debug.Log("Броню не пробить");
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        canWalk = false;
        StartCoroutine(DelayAfterAttack());
    }


    IEnumerator DelayAfterAttack()
    {
        yield return new WaitForSeconds(3);
        canWalk = true;
    }
}
