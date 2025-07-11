using UnityEngine;

public class DamagableTrigger : Trap
{
    
  
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth)
            {
                enemyHealth.ChangeHealth(damage);
            }
        }
    }
}
