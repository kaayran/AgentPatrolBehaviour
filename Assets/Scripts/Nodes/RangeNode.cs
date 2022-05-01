using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes
{
    public class RangeNode : Node
    {
        private readonly NonPlayableCharacter _npc;
        private readonly float _detectionRange;

        public RangeNode(NonPlayableCharacter npc, float detectionRange)
        {
            _detectionRange = detectionRange;
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            var colliders = Physics.OverlapBox(_npc.transform.position, Vector3.one * _detectionRange);

            foreach (var collider in colliders)
            {
                if (!collider.TryGetComponent<Enemy>(out var component)) continue;
                if (component.gameObject == _npc.gameObject) continue;
                _npc.SetCurrentEnemy(component);
                
                return NodeState.Success;
            }

            return NodeState.Failure;
        }
    }
}