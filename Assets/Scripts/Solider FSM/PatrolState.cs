using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IAIState
{
   private readonly AICharacter character;
   private readonly Transform[] waypoints;
   private int currentWaypointIndex = 0;


   public PatrolState(AICharacter character, Transform[] waypoints)
   {
      this.character = character;
      this.waypoints = waypoints;
      Debug.Log(character + "Начал передвижение по точкам");
   }
   
   
   public void Enter()
   {
      MoveToNextWaypoint();
   }

   public void Update()
   {
      if (character.HasReachedDestination())
      {
         currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
         MoveToNextWaypoint();
      }

      Transform enemy = character.FindNearestEnemy();
      if (enemy != null)
      {
         character.ChangeState(new ChaseState(character, enemy));
      }
   }

   public void Exit() { }

   private void MoveToNextWaypoint()
   {
      character.agent.SetDestination(waypoints[currentWaypointIndex].position);
   }
   
    
}
