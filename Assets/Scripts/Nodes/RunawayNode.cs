using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes
{
    public class RunawayNode : Node
    {
        private NonPlayableCharacter _npc;
        private Transform _target;
        private float _maxDistance;
        private float _speed;
        private bool _isRebased = false;

        public RunawayNode(NonPlayableCharacter npc, Transform target, float maxDistance, float speed)
        {
            _npc = npc;
            _target = target;
            _maxDistance = maxDistance;
            _speed = speed;
        }

        public override NodeState Evaluate()
        {
            _npc.SetColor(Color.blue);
            var distance = Vector3.Distance(_npc.transform.position, _target.position);

            if (!(distance > _maxDistance) && _isRebased == false)
            {
                _isRebased = true;
                _npc.Reload();
                _target.transform.position *= 2;
                return NodeState.Success;
            }

            var transformPosition = _target.position - _npc.transform.position;
            _npc.transform.Translate(transformPosition.normalized * (_speed * Time.deltaTime));

            _isRebased = false;
            return NodeState.Running;
        }
    }
}