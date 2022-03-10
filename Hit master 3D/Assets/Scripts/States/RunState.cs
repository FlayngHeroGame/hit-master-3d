using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Run state",menuName = "State/Run state")]
public class RunState : State
{
    private NavMeshAgent agent;
    private Transform target;

    [SerializeField] private float speed = 3;//скорость бега

    public override void Init()
    {
        target = GameController.singleton.waipoints[characterMovement.curWaipoint].transform;
        agent = characterMovement.agent;
        agent.speed = speed;
        agent.isStopped = false;
        agent.ResetPath();
        agent.SetDestination(target.position);
        characterMovement.SetAnimation("Run");
    }

    public override void UpdateState()
    {
        if (Vector3.Distance(characterMovement.transform.position, target.position) < 1f)
        {
            characterMovement.FinishingState();
        }
    }
}
