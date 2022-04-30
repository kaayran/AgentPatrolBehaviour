using Agent;
using BehaviourTrees;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Nodes
{
    public class AttackNode : Node
    {
        private NonPlayableCharacter _npc;

        public AttackNode(NonPlayableCharacter npc)
        {
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            Debug.Log("AttackNode");
            
            _npc.SetColor(Color.red);
            _npc.Shoot();

            return NodeState.Running;
        }
    }
}