﻿using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes
{
    public class StartNode : Node
    {
        private NonPlayableCharacter _npc;
        private bool _isOnBase;

        public StartNode(NonPlayableCharacter npc)
        {
            _npc = npc;
            _npc.EnterBase += OnEnterBase;
            _npc.ExitBase += OnExitBase;
        }

        private void OnEnterBase()
        {
            _isOnBase = true;
            _npc.RefreshSleepTime();
        }

        private void OnExitBase()
        {
            _isOnBase = false;
            _npc.RefreshSleepTime();
        }

        public override NodeState Evaluate()
        {
            if (_isOnBase)
            {
                Debug.Log("I'm On Base");
                _npc.SetColor(Color.black);
                return NodeState.Success;
            }

            Debug.Log("I'm Not On Base");
            return NodeState.Failure;
        }
    }
}