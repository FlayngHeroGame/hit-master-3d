using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Idle state", menuName = "State/Idle state")]
public class IdleState : State
{
    private NavMeshAgent agent;
    public override void Init()
    {
        agent = characterMovement.agent;
        agent.isStopped = true;
        characterMovement.SetAnimation("Idle");
    }

    public override void UpdateState()
    {
        
    }
}
