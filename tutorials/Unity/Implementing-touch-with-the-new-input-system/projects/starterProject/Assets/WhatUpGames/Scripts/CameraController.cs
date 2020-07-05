using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    public Camera MyCamera { get; private set; }
    public float CurrentZoom
    {
        get => currentZoom;
        private set
        {
            currentZoom = value;
            UpdateCameraTarget();
        }
    }

    [Range(2, 20), Tooltip("Speed in which the camera is moved")]
    public float MovementSpeed = 15f;

    [Range(2, 20), Tooltip("Speed in which the camera zooms")]
    public float ZoomSpeed = 2f;

    [Tooltip("This is the Y offset of our focal point. 0 Means we're looking at the ground.")]
    public float LookOffset;

    [Tooltip("The angle that we want the camera to be at.")]
    public float CameraAngle;

    [Tooltip("The default amount the player is zoomed into the game world.")]
    public float DefaultZoom;

    [Range(10f, 20), Tooltip("The most a player can zoom in to the game world.")]
    public float ZoomMax;

    [Range(.5f, 5), Tooltip("The furthest point a player can zoom back from the game world.")]
    public float ZoomMin;

    private const float ZoomAmount = 0.5f;
    private Vector3 moveTargetPosition;
    private Vector3 zoomTargetPosition;
    private float currentZoom;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        //Set the target position to the current location.
        moveTargetPosition = transform.position;

        //Get a reference to the child camera 
        MyCamera = transform.GetComponentInChildren<Camera>();

        //Set the rotation of the child camera based on property specified in Inspector
        MyCamera.transform.rotation = Quaternion.AngleAxis(CameraAngle, Vector3.right);

        CurrentZoom = DefaultZoom;
        MyCamera.transform.position = zoomTargetPosition;

    }

    private void Update()
    {
        //Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, moveTargetPosition, Time.deltaTime * MovementSpeed);

        //Smoothly zoom the camera
        MyCamera.transform.localPosition = Vector3.Lerp(MyCamera.transform.localPosition, zoomTargetPosition, Time.deltaTime * ZoomSpeed);
    }

    /// <summary>
    /// Calculates a new position based on various properties
    /// </summary>
    private void UpdateCameraTarget()
    {
        zoomTargetPosition = (Vector3.up * LookOffset) + (Quaternion.AngleAxis(CameraAngle, Vector3.right) * Vector3.back) * currentZoom;
    }

    /// <summary>
    /// Moves the camera rig to a new position
    /// </summary>
    /// <param name="newPosition">Position that the camera should be moving towards</param>
    public void Move(Vector3 newPosition)
    {
        moveTargetPosition = transform.position + newPosition;
    }

    /// <summary>
    /// Manages the zoom level of the actual camera in increments of half a meter.
    /// </summary>
    /// <param name="zoomOut">Whether the camera should zoom out or in</param>
    public void Zoom(bool zoomOut)
    {
        CurrentZoom = Mathf.Clamp(currentZoom + (zoomOut ? ZoomAmount : -ZoomAmount), ZoomMin, ZoomMax);

    }
}

