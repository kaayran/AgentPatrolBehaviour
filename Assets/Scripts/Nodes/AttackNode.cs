using Agent;
using BehaviourTrees;
using UnityEngine.AI;
using Color = UnityEngine.Color;

namespace Nodes;

public class AttackNode : Node
{
    private NavMeshAgent _agent;
    private NonPlayableCharacter _npc;

    public AttackNode(NonPlayableCharacter npc, NavMeshAgent agent)
    {
        _npc = npc;
        _agent = agent;
    }

    public override NodeState Evaluate()
    {
        _agent.isStopped = true;
        _npc.SetColor(Color.red);

        return NodeState.Running;
    }
}