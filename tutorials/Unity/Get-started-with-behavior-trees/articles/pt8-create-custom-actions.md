# Create custom actions

Actions are the meat of your behavior tree. They are usually either changing functionality behind the scenes and/or are what drive the visual changes of your AI. This demo project will have two actions. One will change functionality behind the scenes and the other will be what moves the AI to a new location. Unlike all the other node types you have created, actions will inherit directly from `Node` so there is no extra base class to create.

## SetNavigationActivityTo 
By now you have probably noticed that `NavigationActivity` is the gate keeper to each of the branches. Your tree will need a way to change that value, which is the purpose of the `SetNavigationActivityTo` class. 

1. Add a new **Actions** folder in **/WUG/Scripts/Behaviors/**. 
2. Create a new class in the **Actions** folder called **SetNavigationActivityTo**, which will inherit from `Node`. 
3. Add the following code:

```csharp
public class SetNavigationActivityTo : Node
{

    private NavigationActivity m_NewActivity;

    public SetNavigationActivityTo(NavigationActivity newActivity)
    {
        m_NewActivity = newActivity;
        Name = $"Set NavigationActivity to {m_NewActivity}";
    }
    protected override void OnReset() { }

    protected override NodeStatus OnRun()
    {
        if (GameManager.Instance == null || GameManager.Instance.NPC == null)
        {
            StatusReason = "GameManager and/or NPC is null";
            return NodeStatus.Failure;
        }

        GameManager.Instance.NPC.MyActivity = m_NewActivity;

        return NodeStatus.Success;
    }
}
```

The constructor will take a new parameter of type `NavigationActivity` and set the private global variable to that value. `OnRun()` will make sure it has a reference to the necessary instances and if so, set the value and return `NodeStatus.Success`. `NodeStatus.Failure` will be returned if the node it lacks the necessary references.

## NavigateToDestination
`NavigateToDestination` will set and monitor the movement of the AI to a new destination via the **NavMeshAgent**. Create a new class in the **Actions** folder called **NavigateToDestination**, and have it inherit from `Node`. Add the following code: 

```csharp
public class NavigateToDestination : Node
{
    private Vector3 m_TargetDestination;

    public NavigateToDestination()
    {
        Name = "Navigate";
    }

    protected override void OnReset() { }
}
```
Notice that the constructor in this case is only setting the `Name` variable. This is because `OnRun()` will communicate with GameManager to obtain the **destination**. Go ahead and add the code for `OnRun()`:

```csharp
protected override NodeStatus OnRun()
{
    //Confirm all references exist
    if (GameManager.Instance == null || GameManager.Instance.NPC == null)
    {
        StatusReason = "GameManager or NPC is null";
        return NodeStatus.Failure;
    }

    //Perform logic that should only run once
    if (EvaluationCount == 0)
    {
        //Get destination from Game Manager 
        GameObject destinationGO = GameManager.Instance.NPC.MyActivity == NavigationActivity.PickupItem ?  GameManager.Instance.GetClosestItem() : GameManager.Instance.GetNextWayPoint();

        //Confirm that the destination is valid - If not, fail.
        if (destinationGO == null)
        {
            StatusReason = $"Unable to find game object for {GameManager.Instance.NPC.MyActivity}";
            return NodeStatus.Failure;
        }

        //Get a valid location on the NavMesh that's near the target destination
        NavMesh.SamplePosition(destinationGO.transform.position, out NavMeshHit hit, 1f, 1);
        
        //Set the location for checks later
        m_TargetDestination = hit.position;

        //Set the destination on the NavMesh. This tells the AI to start moving to the new location.
        GameManager.Instance.NPC.MyNavMesh.SetDestination(m_TargetDestination);
        StatusReason = $"Starting to navigate to {destinationGO.transform.position}";
        
        //Return running, as we want to continue to have this node evaluate
        return NodeStatus.Running;
    }

    //Calculate how far the AI is from the destination
    float distanceToTarget = Vector3.Distance(m_TargetDestination, GameManager.Instance.NPC.transform.position);

    //If the AI is within .25f then navigation will be considered a success
    if (distanceToTarget < .25f)
    {
        StatusReason = $"Navigation ended. " +
            $"\n - Evaluation Count: {EvaluationCount}. " +
            $"\n - Target Destination: {m_TargetDestination}" +
            $"\n - Distance to target: {Math.Round(distanceToTarget, 1)}";

        return NodeStatus.Success;
    }

    //Otherwise, the AI is still on the move
    StatusReason = $"Distance to target: {distanceToTarget}";
    return NodeStatus.Running;

}
```
There is a lot to unpack here! Let us break down the code.

When the node is first run, it will ask the `GameManager` for a target destination depending on what the current `NavigationActivity` of the NPC is. The GameManager will either return the **closest item position** or the **next way point position**. Once there is a position, the node will use [NavMesh.SamplePosition](https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html) to get a position that the NavMeshAgent can navigate to. This will help prevent passing a value to the NavMesh that it is unable to calculate a navigation path to. Finally, the node will return `NodeStatus.Running`. This makes sure that the behavior tree will do another evaluation.

On all subsequent evaluations, the node will calculate the distance between the NPC and the destination. If the NPC has more than 0.25f distance, the node will return `NodeStatus.Running`. Otherwise, the navigation will be considered a complete and the node will return `NodeStatus.Success`.

> This tutorial does not cover the NavMesh system in depth. See the [Unity Manual](https://docs.unity3d.com/Manual/nav-BuildingNavMesh.html) for more information on how the NavMesh works. If you are wondering what is causing the AI to actually move, it's triggered when [NavMeshAgent.SetDestination](https://docs.unity3d.com/ScriptReference/AI.NavMeshAgent.SetDestination.html) is set.

It should be noted that this node is rudimentary. It is missing some failsafe checks, such as whether the NavMeshAgent got stuck, the path failed to recalculate or more. 

### [Previous (Create custom conditions)](./pt7-create-custom-conditions.md)    |     [Next (Building and running the Tree)](./pt9-build-and-run-the-tree.md)



