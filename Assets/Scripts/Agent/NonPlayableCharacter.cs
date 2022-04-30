using System;
using System.Collections.Generic;
using BehaviourTrees;
using Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Agent
{
    public class NonPlayableCharacter : MonoBehaviour
    {
        [SerializeField] private int _ammoCount;
        [SerializeField] private Transform _target;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _maxRange;
    
        private Node _root;
        private Material _material;
        [SerializeField] private float _speed;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
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
            var runawayNode = new RunawayNode(this, _target, _maxDistance, _speed);
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
    }
}