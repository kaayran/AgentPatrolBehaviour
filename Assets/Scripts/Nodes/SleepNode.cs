using Agent;
using BehaviourTrees;
using UnityEngine;

namespace Nodes
{
    public class SleepNode : Node
    {
        private readonly NonPlayableCharacter _npc;

        public SleepNode(NonPlayableCharacter npc)
        {
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            if (!(_npc.GetRemainTime() > 0)) return NodeState.Failure;

            _npc.SetColor(Color.white);
            _npc.Heal();
            return NodeState.Running;
        }
    }
}