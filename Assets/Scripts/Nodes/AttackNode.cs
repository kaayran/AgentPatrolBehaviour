using Agent;
using BehaviourTrees;
using Color = UnityEngine.Color;

namespace Nodes
{
    public class AttackNode : Node
    {
        private readonly NonPlayableCharacter _npc;

        public AttackNode(NonPlayableCharacter npc)
        {
            _npc = npc;
        }

        public override NodeState Evaluate()
        {
            _npc.SetColor(Color.red);
            _npc.Shoot();

            return NodeState.Running;
        }
    }
}