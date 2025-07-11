using System;
using UnityEngine;

public class DarkSword : Weapon
{
    private float currentDamage;
    public override void DamageOther(Collider other)
    {
        currentDamage = attackSword.BaseDamage * damageMultipler;
        Debug.Log("Меч тьмы атакует с уроном: " + currentDamage);
        
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        EnemyMove enemyMove = other.gameObject.GetComponent<EnemyMove>();

        if(enemyHealth) enemyHealth.ChangeHealth(currentDamage);
        if(enemyMove) enemyMove.Stun();
        if(swordHitEffect) Instantiate(swordHitEffect, transform.position, Quaternion.identity);
        
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (attackSword.canAttack)
            {
                DamageOther(other);
            }
            
        }
    }
}
