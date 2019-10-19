# Cleaning up the CameraController class

Now that the project is listening for specific events, we do not have a need to do a validation check for a particular phase before performing the Action. Instead, we can rest assured that the `OnMove()` and `OnZoom()` methods will only be called when we want them to. 

To clean the code, remove the if statement at the beginning that is checking for the proper phase. Your methods should now look like this:

```csharp
        public void OnMove(InputAction.CallbackContext context)
        {
            Vector2 value = context.ReadValue<Vector2>();

            _moveDirection = new Vector3(value.x, 0, value.y);
        }

        public void OnZoom(InputAction.CallbackContext context)
        {
            CurrentZoom = Mathf.Clamp(_currentZoomAmount - context.ReadValue<Vector2>().y, ZoomMax, ZoomMin);
        }

```
### [Listening for callback events](./articles/pt-2-listening-for-callback-events.md)
