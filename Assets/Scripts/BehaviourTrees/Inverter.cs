using System;

namespace BehaviourTrees;

public class Inverter : Node
{
    protected readonly Node _node;

    public Inverter(Node node)
    {
        _node = node;
    }

    public override NodeState Evaluate()
    {
        switch (_node.NodeState)
        {
            case NodeState.Running:
                _nodeState = NodeState.Running;
                break;
            case NodeState.Success:
                _nodeState = NodeState.Failure;
                break;
            case NodeState.Failure:
                _nodeState = NodeState.Success;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return _nodeState;
    }
}