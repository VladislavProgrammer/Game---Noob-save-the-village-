using System;
using UnityEngine;

public class SoliderAttack : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    public bool CanAttack;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            AttackOther();
        }

    }

    void AttackOther()
    {
      
        _anim.SetTrigger("attack");
        CanAttack = true;
    }
}
