# Zooming the camera

As mentioned above, the player can zoom the camera by making a pinching motion with their fingers. Moving the fingers closer together zooms out, while moving them further apart zooms in. You'll add this motion next. Add a new **class variable** to `InputManager` above the `Awake` method:

``` csharp
private float lastMultiTouchDistance;
```

Then add a `ZoomCamera` method below the `MoveCamera` method:

``` csharp

private void ZoomCamera(Touch firstTouch, Touch secondTouch)
{
    //1
    if (firstTouch.phase == TouchPhase.Began || 
      secondTouch.phase == TouchPhase.Began)
    {
        lastMultiTouchDistance = Vector2.Distance(firstTouch.screenPosition, 
          secondTouch.screenPosition);
    }

    //2
    if (firstTouch.phase != TouchPhase.Moved || 
      secondTouch.phase != TouchPhase.Moved)
    {
        return;
    }

    //3
    float newMultiTouchDistance = Vector2.Distance(firstTouch.screenPosition, 
      secondTouch.screenPosition);

    //4
    CameraController.Instance?.Zoom(newMultiTouchDistance < 
      lastMultiTouchDistance);

    //5
    lastMultiTouchDistance = newMultiTouchDistance;
}
```

On the surface, zooming may seem complicated. But fear not, the logic is simple! The `ZoomCamera` determines if the player pinches their fingers closer together or further apart. Here's how it works:

1.  It confirms whether this is the first time a second finger has touched the screen.
2.  Then it makes sure the remaining logic only executes if both fingers are actively moving.
3.  Next, it calculates if fingers are pinching together or apart. `lastMultiTouchDistance` and `newMultiTouchDistance` store the distance between the two fingers.
4.  Then it calls the zoom method on the camera, specifying if it's zooming in or out.
5.  Finally, it sets the `lastMultiTouchDistance` amount for the next time the method runs.

Now it's time to hook up a call to `ZoomCamera`. Add an `else if` statement to `Update`. Replace the contents of the `Update` method with the following:

``` csharp
if (Touch.activeFingers.Count == 1)
{
    MoveCamera(Touch.activeTouches[0]);
}
else if (Touch.activeFingers.Count == 2)
{
    ZoomCamera(Touch.activeTouches[0], 
      Touch.activeTouches[1]);
}
```

`Touch.activeFingers.Count == 2` means two fingers are on the screen, and you can assume they're attempting to zoom. That's it! Save your changes, run your game and pinch your fingers to see the camera in action.

![demo of zoom behavior](../images/zoomDemo.gif)

Now that you've added the touch input camera controls, it's time to add some new features!

### [Previous (The camera setup and movement)](./pt-3-moving-the-camera.md)    |     [Next (Set up the UI for building placement)](./pt-5-setting-up-the-ui-for-building.md)