# AgentPatrolBehaviour

Project to try some things with Behaviour Tree, NavMesh and bots managment

## Текст задания

Составить BehaviourTree (в виде схемы) для NPC, который в спокойном состоянии патрулирует территорию, по окончанию каждого патруля (окончанием считается возвращение в стартовую точку) NPC может или заснуть на 2 минуты или продолжить патруль.
Когда у NPC есть враг, он атакует его, причем стрелять во врага может, только если у него есть патроны и враг на расстоянии меньше 30 метров. Если патронов нет, то NPC убегает от врага.

## Структура дерева с помощью кода

### Базовая нода

```csharp
public abstract class Node
{
    protected NodeState _nodeState;

    public NodeState NodeState => _nodeState;

    public abstract NodeState Evaluate();
}

public enum NodeState
{
    Running,
    Success,
    Failure
}
```

### Декоратор - инвертер

```csharp
public class Inverter : Node
{
    protected readonly Node _node;

    public Inverter(Node node)
    {
        _node = node;
    }

    public override NodeState Evaluate()
    {
        _nodeState = _node.Evaluate() switch
        {
            NodeState.Running => NodeState.Running,
            NodeState.Success => NodeState.Failure,
            NodeState.Failure => NodeState.Success,
            _ => throw new ArgumentOutOfRangeException()
        };

        return _nodeState;
    }
}
```

### Декоратор - случайная нода

```csharp
public class Random : Node
{
    private readonly List<Node> _nodes;

    public Random(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        var randNodeIndex =  UnityEngine.Random.Range(0, _nodes.Count - 1);
        _nodeState = _nodes[randNodeIndex].Evaluate();

        return _nodeState;
    }
}
```

### Селектор

```csharp
public class Selector : Node
{
    protected readonly List<Node> _nodes;

    public Selector(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    _nodeState = NodeState.Running;
                    return _nodeState;
                case NodeState.Success:
                    _nodeState = NodeState.Success;
                    return _nodeState;
                case NodeState.Failure:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        _nodeState = NodeState.Failure;
        return _nodeState;
    }
}
```

### Последовательность

```csharp
public class Sequence : Node
{
    protected readonly List<Node> _nodes;

    public Sequence(List<Node> nodes)
    {
        _nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        var isAnyNodeRunning = false;

        foreach (var node in _nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.Running:
                    isAnyNodeRunning = true;
                    break;
                case NodeState.Success:
                    break;
                case NodeState.Failure:
                    _nodeState = NodeState.Failure;
                    return _nodeState;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        _nodeState = isAnyNodeRunning ? NodeState.Running : NodeState.Success;

        return _nodeState;
    }
}
```
