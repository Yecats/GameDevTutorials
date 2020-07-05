using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


public class InputManager : MonoBehaviour
{
    [Range(10, 100), Tooltip("Speed adjustment for calculating the amount the touch has moved ")]
    public float TouchSpeed = 10f;

    private float lastMultiTouchDistance;
    private bool isBuilding = false;

    private void Awake()
    {
        //Enable support for the new Enhanced Touch API and testing with the mouse
        EnhancedTouchSupport.Enable();
        
        //Uncomment the next line if you are using mouse to simulate touch
        //TouchSimulation.Enable();
    }

    public void Update()
    {
        //Single finger means the player is trying to move around the scene or place an object
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
        //Two fingers means the player is trying to zoom in/out
        else if (Touch.activeFingers.Count == 2)
        {
            ZoomCamera(Touch.activeTouches[0], Touch.activeTouches[1]);
        }
        //No fingers while isBuilding is true means the player was dragging a model and stopped
        else if (Touch.activeFingers.Count == 0 && isBuilding)
        {
            CompleteBuild();
        }
    }

    #region Camera Events

    /// <summary>
    /// Zoom the camera based on pinching movement
    /// </summary>
    /// <param name="firstTouch">Touch data relating to the first finger touching the screen</param>
    /// <param name="secondTouch">Touch data relating to the second finger the screen</param>
    private void ZoomCamera(Touch firstTouch, Touch secondTouch)
    {
        if (firstTouch.phase == TouchPhase.Began || secondTouch.phase == TouchPhase.Began)
        {
            lastMultiTouchDistance = Vector2.Distance(firstTouch.screenPosition, secondTouch.screenPosition);
        }

        // Ensure that remaining logic only executes if either finger is actively moving
        if (firstTouch.phase != TouchPhase.Moved || secondTouch.phase != TouchPhase.Moved)
        {
            return;
        }

        //Calculate if fingers are pinching together or apart
        float newMultiTouchDistance = Vector2.Distance(firstTouch.screenPosition, secondTouch.screenPosition);

        //Call the zoom method on the camera, specifying if it's zooming in our out
        CameraController.Instance?.Zoom(newMultiTouchDistance < lastMultiTouchDistance);

        // Set the last distance calculation
        lastMultiTouchDistance = newMultiTouchDistance;
    }


    /// <summary>
    /// Move the camera based on a touch position
    /// </summary>
    /// <param name="touch">Touch data associated with finger that is touching the screen</param>
    private void MoveCamera(Touch touch)
    {
        // Ensure that remaining logic only executes if the finger is actively moving
        if (touch.phase != TouchPhase.Moved)
        {
            return;
        }

        //Calculate the new camera position based on the current touch position and desired touch speed.
        Vector3 newPosition = new Vector3(-touch.delta.normalized.x, 0, -touch.delta.normalized.y) * 
            Time.deltaTime * TouchSpeed;

        //Pass the new target position to the camera for calculation
        CameraController.Instance?.Move(newPosition);
    }

    #endregion


    #region Build Events

    /// <summary>
    /// UI click event for initiating an object being built
    /// </summary>
    /// <param name="assetName">Model name of the asset the user wants to build</param>
    public void StartBuild_OnPointerDown(GameObject model)
    {
        if (Touch.activeTouches.Count == 0)
        {
            return;
        }

        isBuilding = true;

        BuildManager.Instance?.Build(model, Touch.activeTouches[0].screenPosition);
        
    }

    /// <summary>
    /// Determines if a touch drag has occurred, and if so passes the value to the Build Manager
    /// </summary>
    /// <param name="touch"></param>
    public void DragAsset(Touch touch)
    {
        if (touch.phase != TouchPhase.Moved)
        {
            return;
        }

        BuildManager.Instance?.MoveAsset(touch.screenPosition);
    }

    /// <summary>
    /// Disables build mode and lets the Build Manager know that the player has finished dragging
    /// </summary>
    public void CompleteBuild()
    {
        isBuilding = false;
        BuildManager.Instance?.PlaceAsset();
    }

    #endregion


}
