# Create custom conditions

Conditions can be thought of as the gate keeper for branches. A condition must be passed before the rest of the sibling nodes can be executed. Conditions are usually game specific, and I have yet to encounter a generic one. This demo project will have two conditions.

Like the other node types, you will need to create a `Condition` class to inherit from. Create a new **abstract** class in **/WUG/Scripts/Behaviors** called `Condition`, which will inherit from `Node`. Add the following code:

```csharp
    public abstract class Condition : Node
    {

        public Condition(string name)
        {
            Name = name;
        }

    }
```

There is nothing inherently special about this base class but having it will allow the Behavior Tree Visualizer to stylize all conditions a specific way. It also sets each condition up to easily have custom name set, as you will see shortly.

## IsNavigationActivityTypeOf
The `IsNavigationActivityTypeOf` condition is quite simple - it will look at the `NavigationActivity` property on the NPC class and confirm that it is the desired value. If it is, it will return `NodeStatus.Success` and if not, it returns `NodeStatus.Failure`. It will take one property, which is the `NavigationActivity` to look for.

1. Add a new **Conditions** folder in **/WUG/Scripts/Behaviors/**. 
2. Create a new class in the **Conditions** folder called **IsNavigationActivityTypeOf**, which will inherit from `Condition`. 
3. Add the following global variable and code for the **constructor**:

```csharp
public class IsNavigationActivityTypeOf : Condition
{
    private NavigationActivity m_ActivityToCheckFor;

    public IsNavigationActivityTypeOf(NavigationActivity activity) : base($"Is Navigation Activity {activity}?")
    {
        m_ActivityToCheckFor = activity;
    }
}
```

The constructor will set the global variable `m_ActivityToCheckFor` to the desired value to search for and pass a predefined `Name` to the base class. The name will be displayed on the node when using Behavior Tree Visualizer.

Add the following methods for `OnReset()` and `OnRun()`:

```csharp

protected override void OnReset() { }

protected override NodeStatus OnRun()
{
    if (GameManager.Instance == null || GameManager.Instance.NPC == null)
    {
        StatusReason = "GameManager and/or NPC is null";
        return NodeStatus.Failure;
    }

    StatusReason = $"NPC Activity is {m_ActivityToCheckFor}";

    return GameManager.Instance.NPC.MyActivity == m_ActivityToCheckFor ? NodeStatus.Success : NodeStatus.Failure; 
}
```

`OnRun()` will first check for references to the necessary scripts and if those pass it'll do a check against `NPC.MyActivity` for the value needed.

## AreItemsNearBy
The `AreItemsNearBy` condition will check to see if an item is a specific distance from the player. It will take one property, which is the `maxDistance` to search. Create a new class in the **Conditions** folder called **AreItemsNearBy**, which will inherit from `Condition`. Add the following global variable and code for the **constructor**:

```csharp
public class AreItemsNearBy : Condition
{
    private float m_DistanceToCheck;

    public AreItemsNearBy(float maxDistance) : base($"Are Items within {maxDistance}f?") 
    { 
        m_DistanceToCheck = maxDistance; 
    }
}
```

Add the following methods for `OnReset()` and `OnRun()`:

```csharp
protected override void OnReset() { }

protected override NodeStatus OnRun()
{
    //Check for references
    if (GameManager.Instance == null || GameManager.Instance.NPC == null)
    {
        StatusReason = "GameManager and/or NPC is null";
        return NodeStatus.Failure;
    }

    //Get the closest item
    GameObject item = GameManager.Instance.GetClosestItem();

    //Check to see if something is close by
    if (item == null)
    {
        StatusReason = "No items near by";
        return NodeStatus.Failure;

    }
    else if (Vector3.Distance(item.transform.position, GameManager.Instance.NPC.transform.position) > m_DistanceToCheck)
    {
        StatusReason = $"No items within range of {m_DistanceToCheck} meters";
        return NodeStatus.Failure;
    }

    return NodeStatus.Success;
}
```
`OnRun()` will do the same instance check and if it passes, will call `GameManager.GetClosestItem` for the next item. If that item is `null` or not within the range of `m_DistanceToCheck` then the condition will return `NodeStatus.Failure`. Otherwise, it will return `NodeStatus.Success`.

### [Previous (A closer look at the demo project)](./pt6-closer-look-at-demo-project.md/)    |     [Next (Create custom actions)](./pt8-create-custom-actions.md)

