using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SoliderController : MonoBehaviour
{
   [SerializeField] private float _detectionRadius = 300f;
   [SerializeField] private List<GameObject> currentEnemies = new List<GameObject>();
   [SerializeField] private float _scaningDelay = 5f;
   [SerializeField] private NavMeshAgent _agent;
   [SerializeField] private LayerMask enemyLayerMask;
   public bool CanScuning;
   private NPCWalk npcWalk;
      
   private void OnEnable()
   {
      npcWalk = GetComponent<NPCWalk>();
      _agent = GetComponent<NavMeshAgent>();
      if(CanScuning) TryScaning();
   }

   void CheckOtherEnemies()
   {
      currentEnemies.Clear();
      
      Collider[] otherColliders = Physics.OverlapSphere(transform.position, _detectionRadius, enemyLayerMask);

      for (int i = 0; i < otherColliders.Length; i++)
      {
         GameObject other = otherColliders[i].gameObject;
         if (other.GetComponent<EnemyHealth>())
         {
            currentEnemies.Add(other); // заполняем лист ближайшими врагами
            TargetRandomEnemy();
         }

         else
         {
            Debug.Log("Нет врагов поблизости");
            StartPatrolling();
         }
      }
   }


   
   void TargetRandomEnemy()
   {
      Debug.Log("Атакую случайного врага");
      npcWalk.CanPatrolling = false;
      int rand = UnityEngine.Random.Range(0, currentEnemies.Count);
      GameObject targetEnemy = currentEnemies[rand];
      if (targetEnemy == null)
      {
         StartPatrolling();
      }

      else
      {
         _agent.SetDestination(targetEnemy.transform.position);

      }
      
      
   }

   void StartPatrolling()
   {
      npcWalk.CanPatrolling = true;
   }

   void TryScaning()
   {
      StartCoroutine(ScaningDelay());
   }

   private IEnumerator ScaningDelay()
   {
      CheckOtherEnemies();
      yield return new WaitForSeconds(_scaningDelay);
      if(CanScuning) StartCoroutine(ScaningDelay());
   }
}
