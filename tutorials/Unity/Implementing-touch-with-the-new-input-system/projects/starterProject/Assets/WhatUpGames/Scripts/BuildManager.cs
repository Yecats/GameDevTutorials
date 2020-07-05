using System.Linq;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    //Stores a reference to the game object that is being built
    private GameObject activeModel;
    //Speed the game object moves to the new target position
    private const float MoveSpeed = 8;
    // Layers that raycast should look for in bitshifted format
    private int physicsLayers = (1 << 8) | (1 << 9);
    //Position the activeModel will be moved to via Update
    private Vector3 targetPosition;

    //
    private bool canPlaceHere;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Update()
    {
        //If a model is active in the world then calculate lerp for movement
        if (activeModel)
        {
            activeModel.transform.position = Vector3.Lerp(activeModel.transform.position, targetPosition, Time.deltaTime * MoveSpeed);
        }
    }

    /// <summary>
    /// Instantiates the asset into the game world
    /// </summary>
    /// <param name="model">Prefab to instantiate </param>
    /// <param name="screenPosition">Location to instantiate</param>
    public void Build(GameObject model, Vector3 screenPosition)
    {
        CalculatePosition(screenPosition);

        activeModel = Instantiate(model, targetPosition, Quaternion.identity);
    }

    /// <summary>
    /// Calculates the new location of the model 
    /// </summary>
    /// <param name="screenPosition">New position as screen space</param>
    public void MoveAsset(Vector3 screenPosition)
    {
        CalculatePosition(screenPosition);
    }

    /// <summary>
    /// Places the model in the world if it has been positioned in a valid location (a world tile)
    /// </summary>
    public void PlaceAsset()
    {
        if (canPlaceHere)
        {
            activeModel = null;
        }
        else
        {
            Destroy(activeModel);
        }
    }

    /// <summary>
    /// Helper method that calculates and sets the target position for the model to lerp to
    /// </summary>
    /// <param name="screenPosition">New position as screen space</param> 
    private void CalculatePosition(Vector3 screenPosition)
    {
        if (!CameraController.Instance) { return; }
        
        //Calculates the direction of the Raycast based on the screenPosition of the touch
        Ray directionOfTouch = CameraController.Instance.MyCamera.ScreenPointToRay(screenPosition);

        //Does a raycast for all objects that are layers 8 and 9 and then orders the results by layer
        RaycastHit[] hitPoints = Physics.RaycastAll(directionOfTouch.origin, directionOfTouch.direction, 
            Mathf.Infinity, physicsLayers).OrderBy(x => x.transform.gameObject.layer).ToArray();

        if (hitPoints.Length == 0) { return; }

        //Can only ever hit up to two colliders. If two have been hit, the last one will be the tile
        int index = hitPoints.Length == 2 ? 1 : 0;

        //The model can only exist if it is being placed on the world tile
        canPlaceHere = hitPoints.Length == 2;

        //For visually debugging in the scene
        Debug.DrawRay(hitPoints[index].point, Vector3.up * 2f, Color.cyan, 0.5f);
        Debug.DrawLine(directionOfTouch.origin, hitPoints[index].point, Color.yellow, 0.5f);

        //Set the calculated target position
        targetPosition = hitPoints[index].point;

    }
}
