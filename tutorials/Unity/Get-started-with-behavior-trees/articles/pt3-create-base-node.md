# Create the base node
The first thing that you will need is the base node that all of the other nodes will derive from. 

1. Add a new folder in **Wug/Scripts/** called **Behaviors**.
2. Create a new abstract C# script called **Node** and have it implement **NodeBase**.

> `NodeBase` is part of Behavior Tree Visualizer. It provides access to the `NodeStatus` enum which you will use for the return status codes. It also has other useful properties that you will use later. 

You should now have this:

```csharp
using WUG.BehaviorTreeVisualizer;

public abstract class Node : NodeBase
{

}
```

`Node` needs to have the ability to do two things - **Run** the logic and **Reset** the logic. There is a set of base things that will happen for every single node, which will occur in `Run` and `Reset`. `OnRun` and `OnReset` will be overrode by each derived node and will contain the custom logic. 

Add the following code to `Node`:

```csharp
//Keeps track of the number of times the node has been evaluated in a single 'run'.
public int EvaluationCount;

// Runs the logic for the node
public virtual NodeStatus Run()
{
    //Runs the 'custom' logic
    NodeStatus nodeStatus = OnRun();

    //Increments the tracker for how many times the node has been evaluated this 'run'
    EvaluationCount++;

    // If the nodeStatus is not Running, then it is Success or Failure and can be Reset
    if (nodeStatus != NodeStatus.Running)
    {
        Reset();
    }

    //Return the StatusResult.
    return nodeStatus;
}

public void Reset()
{
    EvaluationCount = 0;
    OnReset();
}

protected abstract NodeStatus OnRun();
protected abstract void OnReset();
```

That is it for the base class! Now it is time to create some composites. 

### [Previous (Understanding Behavior Trees)](./pt2-understanding-behavior-trees.md)    |     [Next (Create General Composites)](./pt4-create-general-composites.md)

