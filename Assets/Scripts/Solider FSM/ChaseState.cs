using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IAIState
{
    private readonly AICharacter character;
    private readonly Transform enemy;
    
    public ChaseState(AICharacter character, Transform enemy)
    {
        this.character = character;
        this.enemy = enemy;
        Debug.Log(character + "Начал погоню за" + enemy);
    }

    
    public void Enter()
    {
        character.agent.stoppingDistance = 3f; // Дистанция для атаки
    }
    
    
    public void Update()
    {
        // Если враг ушел за пределы радиуса обнаружения
        if (enemy == null || Vector3.Distance(character.transform.position, enemy.position) > character.detectionRadius * 1.2f)
        {
            character.ChangeState(new PatrolState(character, character.waypoints));
            return;
        }

        // Преследование врага
        character.agent.SetDestination(enemy.position);

        // Если достигли врага - атакуем
        if (character.HasReachedDestination())
        {
            character.Attack();
        }
    }
    
    public void Exit()
    {
        character.agent.stoppingDistance = 0.5f; // Возвращаем стандартное значение
    }

}
