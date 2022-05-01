using Agent;
using BehaviourTrees;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes
{
    public class RunawayNode : Node
    {
        private readonly NonPlayableCharacter _npc;
        private readonly NavMeshAgent _agent;
        private readonly Transform _target;
        private readonly float _maxDistance;

        public RunawayNode(NonPlayableCharacter npc, Transform target, float maxDistance)
        {
            _npc = npc;
            _target = target;
            _maxDistance = maxDistance;
            _agent = npc.GetAgent();
        }

        public override NodeState Evaluate()
        {
            _npc.SetColor(Color.blue);

            var distance = Vector3.Distance(_agent.transform.position, _target.transform.position);

            if (distance > _maxDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target.transform.position);

                return NodeState.Running;
            }

            _agent.isStopped = true;
            _npc.Reload();

            return NodeState.Success;
        }
    }
}