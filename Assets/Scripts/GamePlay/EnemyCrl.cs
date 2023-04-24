using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyCrl : Character
{
    private IdleState idle = new IdleState();
    private MoveState move = new MoveState();
    private AttackState attack = new AttackState();
    private DeadState dead = new DeadState();

    public BaseState currentState;
    public NavMeshAgent agent;
    private void Awake()
    {
        currentState = idle;
    }
    void Update()
    {
        
    }

    private void ChangeState(BaseState newState)
    {
        currentState = newState;
    }
}
