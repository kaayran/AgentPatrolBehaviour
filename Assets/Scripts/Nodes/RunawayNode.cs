using Agent;
using BehaviourTrees;
using Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes
{
    public class RunawayNode : Node
    {
        private readonly NonPlayableCharacter _npc;
        private readonly NavMeshAgent _agent;
        private readonly float _maxDistance;
        private Vector3 _target;

        public RunawayNode(NonPlayableCharacter npc, float maxDistance)
        {
            _npc = npc;
            _maxDistance = maxDistance;
            _agent = npc.GetAgent();

            _target = NavPointGenerator.GetRandomPoint();
        }

        public override NodeState Evaluate()
        {
            _npc.SetColor(Color.blue);

            var distance = Vector3.Distance(_agent.transform.position, _target);

            if (distance > _maxDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target);

                return NodeState.Running;
            }

            _agent.isStopped = true;
            _target = NavPointGenerator.GetRandomPoint();
            Debug.Log(_target);
            _npc.Reload();

            return NodeState.Success;
        }
    }
}