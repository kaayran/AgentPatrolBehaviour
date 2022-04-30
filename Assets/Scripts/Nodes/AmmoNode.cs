using Agent;
using BehaviourTrees;

namespace Nodes;

public class AmmoNode : Node
{
    private NonPlayableCharacter _npc;

    public AmmoNode(NonPlayableCharacter npc)
    {
        _npc = npc;
    }

    public override NodeState Evaluate()
    {
        return _npc.GetCurrentAmmo() > 0 ? NodeState.Success : NodeState.Failure;
    }
}