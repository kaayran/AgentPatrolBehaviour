﻿using System.Linq;
using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes
{
    public class RangeNode : Node
    {
        private NonPlayableCharacter _npc;
        private float _maxRange;

        public RangeNode(NonPlayableCharacter npc, float maxRange)
        {
            _maxRange = maxRange;
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("RangeNode");
            var colliders = Physics.OverlapBox(_npc.transform.position, Vector3.one * _maxRange);

            foreach (var collider in colliders)
            {
                if (collider.TryGetComponent<Enemy>(out var component))
                {
                    Debug.Log(component.name);
                    return NodeState.Success;
                }
            }
            
            return NodeState.Failure;
        }
    }
}