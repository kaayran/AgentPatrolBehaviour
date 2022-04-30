using System;
using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Selector : Node
    {
        protected readonly List<Node> _nodes;

        public Selector(List<Node> nodes)
        {
            _nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            foreach (var node in _nodes)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Running:
                        _nodeState = NodeState.Running;
                        return _nodeState;
                    case NodeState.Success:
                        _nodeState = NodeState.Success;
                        return _nodeState;
                    case NodeState.Failure:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            _nodeState = NodeState.Failure;
            return _nodeState;
        }
    }
}