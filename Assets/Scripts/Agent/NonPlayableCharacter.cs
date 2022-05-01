using System;
using System.Collections.Generic;
using BehaviourTrees;
using Nodes;
using UnityEngine;
using UnityEngine.AI;
using Random = BehaviourTrees.Random;

namespace Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NonPlayableCharacter : MonoBehaviour
    {
        public Action EnterBase;
        public Action ExitBase;

        [SerializeField] private int _ammoCount;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxRange;
        [SerializeField] private float _speed;
        [SerializeField] private float _timeToSleep;

        private int _maxAmmo;
        private float _remainTime;
        private NavMeshAgent _agent;
        private Material _material;
        private Node _root;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
            _remainTime = _timeToSleep;
            _maxAmmo = _ammoCount;

            SetColor(Color.yellow);

            ConstructTree();
        }

        private void Update()
        {
            _root.Evaluate();
        }

        private void OnTriggerEnter(Collider other)
        {
            EnterBase?.Invoke();
            Debug.Log($"On Base: {other.name}");
        }

        private void OnTriggerExit(Collider other)
        {
            ExitBase?.Invoke();
            Debug.Log($"Going From Base: {other.name}");
        }

        private void ConstructTree()
        {
            var ammoNode = new AmmoNode(this);
            var notAmmoNode = new Inverter(ammoNode);
            var runawayNode = new RunawayNode(this, _maxDistance);
            var attackNode = new AttackNode(this);
            var rangeNode = new RangeNode(this, _maxRange);
            var sleepNode = new SleepNode(this);
            var patrolNode = new PatrolNode(this);
            var startNode = new StartNode(this);

            var runawaySequence = new Sequence(new List<Node> {notAmmoNode, runawayNode});
            var attackSequence = new Sequence(new List<Node> {rangeNode, attackNode});
            var randomSelector = new Random(new List<Node> {sleepNode, patrolNode});
            var startSequence = new Sequence(new List<Node> {startNode, randomSelector});
            var patrolSelector = new Selector(new List<Node> {startSequence, patrolNode});

            _root = new Selector(new List<Node> {runawaySequence, attackSequence, patrolSelector});
        }

        public int GetCurrentAmmo()
        {
            return _ammoCount;
        }

        public void SetColor(Color color)
        {
            _material.color = color;
        }

        public void Shoot()
        {
            _ammoCount--;
        }

        public void Reload()
        {
            _ammoCount = _maxAmmo;
        }

        public NavMeshAgent GetAgent()
        {
            return _agent;
        }

        public float GetTimeToSleep()
        {
            return _timeToSleep;
        }

        public float GetSpeed()
        {
            return _speed;
        }

        public float GetMaxDistance()
        {
            return _maxDistance;
        }

        public float GetRemainTime()
        {
            return _remainTime--;
        }

        public void RefreshSleepTime()
        {
            _remainTime = _timeToSleep;
        }
    }
}