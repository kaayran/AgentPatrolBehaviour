using System.Collections.Generic;
using BehaviourTrees;
using Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NonPlayableCharacter : MonoBehaviour
    {
        [SerializeField] private int _ammoCount;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxRange;
        [SerializeField] private float _speed;

        private NavMeshAgent _agent;
        private Material _material;
        private Node _root;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
            _agent = GetComponent<NavMeshAgent>();
            _agent.speed = _speed;
            
            ConstructTree();
        }

        private void Update()
        {
            _root.Evaluate();

            if (_root.NodeState == NodeState.Failure)
            {
                _material.color = Color.yellow;
            }
        }

        private void ConstructTree()
        {
            var ammoNode = new AmmoNode(this);
            var notAmmoNode = new Inverter(ammoNode);
            var runawayNode = new RunawayNode(this, _maxDistance);
            var attackNode = new AttackNode(this);
            var rangeNode = new RangeNode(this, _maxRange);

            var runawaySequence = new Sequence(new List<Node> {notAmmoNode, runawayNode});
            var attackSequence = new Sequence(new List<Node> {rangeNode, attackNode});

            _root = new Selector(new List<Node> {runawaySequence, attackSequence});
        }

        public int GetCurrentAmmo()
        {
            return _ammoCount;
        }

        public float GetRangeToEnemy()
        {
            throw new System.NotImplementedException();
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
            _ammoCount += 100;
        }

        public NavMeshAgent GetAgent()
        {
            return _agent;
        }
    }
}