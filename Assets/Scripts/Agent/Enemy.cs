using Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speed;

        private NavMeshAgent _agent;
        private Vector3 _target;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = NavPointGenerator.GetRandomPoint();
            
            _agent.speed = _speed;
        }

        private void Update()
        {
            var distance = Vector3.Distance(_agent.transform.position, _target);
            Debug.Log(distance);

            if (distance > _maxDistance)
            {
                _agent.isStopped = false;
                _agent.SetDestination(_target);
            }
            else
            {
                _agent.isStopped = true;
                _target = NavPointGenerator.GetRandomPoint();
                Debug.Log(_target);
            }
        }
    }
}