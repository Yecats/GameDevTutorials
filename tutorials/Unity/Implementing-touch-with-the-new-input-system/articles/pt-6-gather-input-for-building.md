# Gather the building input

The project already includes a `BuildManager` script. Again, take a moment to look at what each method does. Here's a breakdown of the key points:

-   **Awake**: Makes sure there's only one instance of the script in the scene.
-   **Update**: If building, it smoothly moves the prefab each frame.
-   **Build**: Instantiates the prefab into the game world.
-   **MoveAsset**: Calculates the new location of the prefab.
-   **PlaceAsset**: Places the model in the world if it has a valid location, a world tile.
-   **CalculatePosition**: Calculates and sets the target position for the model to lerp to.

Most of these methods work behind the scenes. The only two you need to interact with are `MoveAsset` and `Build`. `InputManager` calls `MoveAsset`. The **UI Event System** calls `Build`. Now that you understand how `BuildManager` operates, it's time to add the last bit of code to `InputManager`.

## Gathering the input

`InputManager` interprets the players intent through their touch actions. There are three stages in total:

1.  **Start Build**: A UI `OnPointerDown` event that starts the building process. It passes the details to `BuildManager.Build`.
2.  **Drag**: Determines if a touch drag has occurred, and if so, passes the value to `BuildManager.MoveAsset`.
3.  **Complete Build**: Disables build mode. Passes the details to `BuildManager.PlaceAsset`.

Add each of the stages. First add a new **class variable** above the `Awake` method:

``` csharp
private bool isBuilding;
```

Second, add a new **method**:

``` csharp

public void StartBuildOnPointerDown(GameObject model)
{
    if (Touch.activeTouches.Count == 0)
    {
        return;
    }

    isBuilding = true;

    BuildManager.Instance?.Build(model, 
      Touch.activeTouches[0].screenPosition);
}
```

`StartBuildOnPointerDown` is called through an **Event Trigger** setup on the **Well** and **WoodCutterLodge** UI game objects. If the finger is still touching the screen, then `isBuilding` is set to true for tracking. It wraps up by passing the **model** and **screenPosition** of the finger to `BuildManager.Build` for processing. Finally, add the `DragAsset` and `CompleteBuild` methods:

``` csharp
 public void DragAsset(Touch touch)
{
    if (touch.phase != TouchPhase.Moved)
    {
        return;
    }

    BuildManager.Instance?.MoveAsset(touch.screenPosition);
}

public void CompleteBuild()
{
    isBuilding = false;
    BuildManager.Instance?.PlaceAsset();
}
```

Here's what these methods do:

1.  `DragAsset` is a pass through method. It uses `TouchPhase.Moved` to confirm the finger moved during the frame. Then it passes the new **screenPosition** to `BuildManager.MoveAsset` for processing.
2.  `CompleteBuild` is only called if the finger leaves the screen. It toggles off build mode and notifies `BuildManager`.

There's one final change: You need to update `Update`. :] Replace all the code in `Update` with the following:

``` csharp
//1
if (Touch.activeFingers.Count == 1)
{
    if (isBuilding)
    {
        DragAsset(Touch.activeTouches[0]);
    }
    else
    {
        MoveCamera(Touch.activeTouches[0]);
    }
}
//2
else if (Touch.activeFingers.Count == 2)
{
    ZoomCamera(Touch.activeTouches[0], Touch.activeTouches[1]);
}
//3
else if (Touch.activeFingers.Count == 0 && isBuilding)
{
    CompleteBuild();
}
```

Now `Update` looks for three key conditions:

-   **One finger is touching**: The player is either trying to Move the camera or drag a prefab for building.
-   **Two fingers are touching**: The player is trying to zoom.
-   **No fingers are touching and they were building**: They are trying to place an object.

Save your changes and return to the Unity editor.

### [Previous (Set up the UI for building placement)](./pt-5-setting-up-the-ui-for-building.md)    |     [Next (Finish setting up the building UI)](./pt-7-finish-setting-up-the-ui.md)