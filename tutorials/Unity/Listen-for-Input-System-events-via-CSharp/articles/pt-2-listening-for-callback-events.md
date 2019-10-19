# Listening for callback events

To keep things clean and central, we are going to plan for the future and have a general `InputManager` class that can listen for all events under our Player Action Map, not just those related to the camera. This means that we'll register a listener for callbacks in `InputManager` and once triggered, we will have the `CameraController` class to take action.

To get started:

1. Under the scripts folder, right click and go to **Create** > **C# Script**. Name it `InputManager`.
2. Select the **GameManager** game object and click **Add Component** > **InputManager** to add it.

Add two global variables - one for the `CameraController` and one for `PlayerInputMapping`. Both of these will be instantiated within `Awake()`.

Your script should look like this:

```csharp
using Assets.Scripts;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CameraController CameraController;
    private PlayerInputMapping _input;

    void Awake()
    {
        _input = new PlayerInputMapping();

        if (CameraController == null)
        {
            CameraController = FindObjectOfType<CameraController>();
        }
    }

}
```

> Reminder: `PlayerInputMapping` is a reference to the C# file that was generated in the previous step. 

There are three events that an Action can trigger. They are:

1. **Started**: An Input Binding started an Interaction with the Action.
2. **Performed**: An Interaction with the Action has been completed.
3. **Canceled**: An Interaction with the Action has been canceled.

All of our Camera actions will care about **performed** and **canceled** events, with the exception of `OnZoom()`, which only cares about **performed**.  

Add the following code to the end of `Awake()` to register the listeners:

```csharp

    _input.Player.Camera_Move.performed += CameraController.OnMove;
    _input.Player.Camera_Move.canceled += CameraController.OnMove;

    _input.Player.Camera_Rotate.performed += CameraController.OnRotate;
    _input.Player.Camera_Rotate.canceled += CameraController.OnRotate;

    _input.Player.Camera_Rotate_Toggle.performed += CameraController.OnRotateToggle;
    _input.Player.Camera_Rotate_Toggle.canceled += CameraController.OnRotateToggle;

    _input.Player.Camera_Zoom.performed += CameraController.OnZoom;
```

If you were to push play now, you'll see that nothing happens. This is because you also need to enable the Action with the Input System as well. This can be done at the individual level via the Action or in bulk via the Action Map. Since we do not have a reason to toggle the active state of the actions, we’ll do it on the Action Map level. 

Add the following code for `OnEnable()` and, because we're good citizens, `OnDisable()`:

```csharp
    void OnEnable()
    {
        //Enable all actions under the Player action map
        // You can do this on an individual action level by calling _input.Player.Camera_Move.Enable() instead.
        _input.Player.Enable();
    }

    void OnDisable()
    {
        //Disable all actions under the Player action map
        _input.Player.Disable();
    }
```

> When you enable an action, the Input System will resolve its bindings if it hasn't already. You can read more on Actions [here](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Actions.html#using-actions) and Binding Resolution [here](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/ActionBindings.html#binding-resolution).

### [Refactoring and setting up the project](./pt-1-refactoring-project.md)     |     [Next (Cleaning up the CameraController class)](./pt-3-cleaning-up-camera-controller.md)
