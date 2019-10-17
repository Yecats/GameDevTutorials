# Adding rotation behavior

Rotating the camera is a two-step process. First, you’ll need to know whether the player is telling you to rotate. This will be done by monitoring whether the Right Mouse button is pushed. If it is then we’ll process the mouse position to tell our game which way to rotate.

Monitoring for a button push is very simple. You just need to read if a float value is 0 (off) or 1 (on). To do this, add the following global variables and `OnRotateToggle()` method to your project:

```csharp
    //Rotation variables
    private bool _rightMouseDown = false;
    private const float InternalRotationSpeed = 4;
    private Quaternion _rotationTarget;
    private Vector2 _mouseDelta;

    /// <summary>
    /// Sets whether the player has the right mouse button down
    /// </summary>
    /// <param name="context"></param>
    public void OnRotateToggle(InputAction.CallbackContext context)
    {
        _rightMouseDown = context.ReadValue<float>() == 1;
    }

```
Add a new `OnRotate()` method to your project to rotate the camera if the right mouse button is pushed:

```csharp
    /// <summary>
    /// Sets the rotation target quaternion if the right mouse button is pushed when the player is moving the mouse
    /// </summary>
    /// <param name="context"></param>
    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!_rightMouseDown)
        {
            return;
        }
        _mouseDelta = context.ReadValue<Vector2>();

        //Sets the rotation target so that we can Slerp to it in LateUpdate
        _rotationTarget *= Quaternion.AngleAxis(_mouseDelta.x * Time.deltaTime * RotationSpeed, Vector3.up);
    }
```

Lastly, add logic to `LateUpdate()` and `Start()` to tell it to rotate the camera:

```csharp

    void Start()
    {
        //Store a reference to the camera rig
        _actualCamera = GetComponentInChildren<Camera>();

        //Set the rotation of the camera based on the CameraAngle property
        _actualCamera.transform.rotation = Quaternion.AngleAxis(CameraAngle, Vector3.right);

        //Set the position of the camera based on the look offset, angle and default zoom properties. This will make sure we're focusing on the right focal point.
        CurrentZoom = DefaultZoom;
        _actualCamera.transform.position = _cameraPositionTarget;

        //Set the initial rotation value
        _rotationTarget = transform.rotation;

    }

    private void LateUpdate()
    {
        //Lerp the camera rig to a new move target position
        transform.position = Vector3.Lerp(transform.position, _moveTarget, Time.deltaTime * InternalMoveSpeed);

        //Move the _actualCamera's local position based on the new zoom factor
        _actualCamera.transform.localPosition = Vector3.Lerp(_actualCamera.transform.localPosition, _cameraPositionTarget, Time.deltaTime * _internalZoomSpeed);

       //Slerp the camera rig's rotation based on the new target
        transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, Time.deltaTime * InternalRotationSpeed);
    }

```

Hook up the logic to the Input System for the new methods:

1.	Under the **Camera_Rotate** event, reference the **CameraController** game object and set the event to `CameraController.OnRotate`.
1.	Under the **Camera_Rotate_Toggle** event, reference the **CameraController** game object and set the event to `CameraController.OnRotateToggle`.
2.	Press play and hold down the right mouse button while you move the mouse around.

That’s it! You should now have a fully functional camera that rotates, zooms and pans around the scene with the new Input System.

![Final Rotation Sample](..\images\pt-7-1-rotate-sample.gif)

### [Previous (Adding zoom behavior)](.\pt-6-adding-zoom-behavior.md)
