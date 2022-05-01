using System.Collections.Generic;

namespace BehaviourTrees
{
    public class Random : Node
    {
        private readonly List<Node> _nodes;

        public Random(List<Node> nodes)
        {
            _nodes = nodes;
        }

        public override NodeState Evaluate()
        {
            var randNodeIndex =  UnityEngine.Random.Range(0, _nodes.Count - 1);
            _nodeState = _nodes[randNodeIndex].Evaluate();

            return _nodeState;
        }
    }
}