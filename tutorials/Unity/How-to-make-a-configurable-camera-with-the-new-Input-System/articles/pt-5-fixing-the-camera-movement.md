# Fixing the camera movement

Well, that's not quite "it" - This is not the behavior that we want. The player should be able to hold down a key and see the camera move constantly in that direction. The reason this happens because the Input System is only sending an event when the key is pressed. It does not have an easy way to monitor for a key being pressed. We’ll need to handle this ourselves. 

> Input Bindings have the concept of Interactions, one of which is called “Hold”. The purpose of this interaction is to trigger the action after a period of time has gone by. It does not trigger the action continuously while the button is held down. 
> 
> You can read more on interactions [here](https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Interactions.html#predefined-interactions).

Fortunately, fixing this is easy! We’ll just need to move the last line from `OnMove()` into `FixedUpdate()`. Your methods should now look like this:

```csharp
    public void OnMove(InputAction.CallbackContext context)
    {
        //Read the input value that is being sent by the Input System
        Vector2 value = context.ReadValue<Vector2>();

        //Store the value as a Vector3, making sure to move the Y input on the Z axis.
        _moveDirection = new Vector3(value.x, 0, value.y);
    }

    private void FixedUpdate()
    {
        //Sets the move target position based on the move direction. Must be done here as there's no logic for the input system to calculate holding down an input
        _moveTarget += (transform.forward * _moveDirection.z + transform.right * _moveDirection.x) * Time.fixedDeltaTime * InternalMoveTargetSpeed;
    }

```

Press play! Our camera movement is now very smooth and handles direction changes beautifully:

![Movement Example](../images/pt-5-1-FixMove.gif)

### [Previous (Hooking it up to code)](./pt-4-hooking-it-up-to-code.md) | [Next (Adding zoom behavior)](./pt-6-adding-zoom-behavior.md)

