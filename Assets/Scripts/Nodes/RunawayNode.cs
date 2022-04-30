using Agent;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes;

public class RunawayNode : Node
{
    private Transform _target;
    private NavMeshAgent _agent;
    private float _maxDistance;
    private NonPlayableCharacter _npc;

    public RunawayNode(NonPlayableCharacter npc, Transform target, NavMeshAgent agent, float maxDistance)
    {
        _npc = npc;
        _target = target;
        _agent = agent;
        _maxDistance = maxDistance;
    }

    public override NodeState Evaluate()
    {
        _npc.SetColor(Color.blue);
        var distance = Vector3.Distance(_agent.transform.position, _target.position);

        if (distance > _maxDistance)
        {
            _agent.isStopped = false;
            _agent.SetDestination(_target.position);

            return NodeState.Running;
        }

        _agent.isStopped = true;

        return NodeState.Success;
    }
}