using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes;

public class RangeNode : Node
{
    private NonPlayableCharacter _npc;
    private Transform _origin;
    private Transform _target;
    private float _maxRange;

    public RangeNode(NonPlayableCharacter npc, float maxRange, Transform target, Transform origin)
    {
        _maxRange = maxRange;
        _target = target;
        _origin = origin;
        _npc = npc;
    }

    public override NodeState Evaluate()
    {
        var distance = Vector3.Distance(_origin.position, _target.position);

        return distance <= _maxRange ? NodeState.Success : NodeState.Failure;
    }
}