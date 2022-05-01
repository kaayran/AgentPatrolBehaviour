using Agent;
using BehaviourTrees;
using Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Nodes
{
    public class PatrolNode : Node
    {
        private readonly NonPlayableCharacter _npc;
        private readonly NavMeshAgent _agent;
        private readonly float _maxDistance;
        private Vector3 _target;

        public PatrolNode(NonPlayableCharacter npc)
        {
            _npc = npc;
            _agent = _npc.GetAgent();
            _maxDistance = _npc.GetMaxDistance();

            _target = NavPointGenerator.GetRandomPoint();
        }

        public override NodeState Evaluate()
        {
            var distance = Vector3.Distance(_agent.transform.position, _target);

            if (distance > _maxDistance)
            {
                Debug.Log("OnPatrol");
                _npc.SetColor(Color.yellow);
                _agent.isStopped = false;
                _agent.SetDestination(_target);

                return NodeState.Running;
            }

            Debug.Log("GetOnPoint");
            _agent.isStopped = true;
            _target = NavPointGenerator.GetRandomPoint();

            return NodeState.Success;
        }
    }
}