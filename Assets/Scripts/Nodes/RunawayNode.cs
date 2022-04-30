using Agent;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes
{
    public class RunawayNode : Node
    {
        private NonPlayableCharacter _npc;
        private Transform _target;
        private float _maxDistance;
        private float _speed;

        public RunawayNode(NonPlayableCharacter npc, Transform target, float maxDistance, float speed)
        {
            _npc = npc;
            _target = target;
            _maxDistance = maxDistance;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("RunawayNode");
            _npc.SetColor(Color.blue);
            var distance = Vector3.Distance(_npc.transform.position, _target.position);

            if (distance > _maxDistance)
            {
                _npc.transform.Translate((_target.position - _npc.transform.position).normalized *
                                         (_speed * Time.deltaTime));

                return NodeState.Running;
            }
            
            return NodeState.Success;
        }
    }
}