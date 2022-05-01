﻿using System;
using Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Enemy : MonoBehaviour
    {
        public Action<float> HealthUpdate;
        
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _speed;
        [SerializeField] private float _health;

        private NavMeshAgent _agent;
        private float _maxHealth;
        private Vector3 _target;

        public float MaxHealth => _maxHealth;

        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _target = NavPointGenerator.GetRandomPoint();

            _maxHealth = _health;
            HealthUpdate?.Invoke(_maxHealth);
        }

        private void Update()
        {
            // var distance = Vector3.Distance(_agent.transform.position, _target);
            //
            // if (distance > _maxDistance)
            // {
            //     _agent.isStopped = false;
            //     _agent.SetDestination(_target);
            // }
            // else
            // {
            //     _agent.isStopped = true;
            //     _target = NavPointGenerator.GetRandomPoint();
            // }
        }

        public void Damage()
        {
            _health--;
            HealthUpdate?.Invoke(_health);

            if (_health <= 0)
            {
                Destroy(gameObject);
            }
        }

        public void Heal()
        {
            if (!(_health < _maxHealth)) return;
            _health++;
            
            HealthUpdate?.Invoke(_health);
        }
    }
}