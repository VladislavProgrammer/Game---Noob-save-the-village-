using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private float _damage;
    
   private void OnTriggerEnter(Collider other)
       {
           if (other.gameObject.tag == "Player")
           {
               var playerHealth = other.gameObject.GetComponent<PlayerHealth>();
               if (playerHealth) playerHealth.RemoveHealth(_damage);
             
           }
       }
    
   
}
