using System;

namespace BehaviourTrees
{
    public class Inverter : Node
    {
        protected readonly Node _node;

        public Inverter(Node node)
        {
            _node = node;
        }

        public override NodeState Evaluate()
        {
            _nodeState = _node.Evaluate() switch
            {
                NodeState.Running => NodeState.Running,
                NodeState.Success => NodeState.Failure,
                NodeState.Failure => NodeState.Success,
                _ => throw new ArgumentOutOfRangeException()
            };

            return _nodeState;
        }
    }
}