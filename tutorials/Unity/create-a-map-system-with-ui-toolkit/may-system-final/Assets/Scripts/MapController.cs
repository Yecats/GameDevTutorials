using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    public GameObject Player;
    private VisualElement _root;
    private VisualElement _playerRepresentation;
    private VisualElement _mapContainer;
    private VisualElement _mapImage;

    [Range(1, 15)]
    public float miniMultiplyer = 1f;
    [Range(1, 15)]
    public float fullMultiplyer = 1f;
    private bool _mapFaded;
    public bool MapFaded
    {
        get => _mapFaded;

        set
        {
            //Do nothing if the value is the same
            if (_mapFaded == value)
            {
                return;
            }

            //Calculate the end value of the background img tint color
            Color end = !_mapFaded ? Color.white.WithAlpha(.5f) : Color.white;

            //Animate the background tint color.
            //Start value: Whatever the current style is set to
            //End value: Alpha is either 50% or 100%, depending on if the player is moving and if the window is open
            //Duration: 500 MS
            //Action: Applies current value to the backgroundImageTintColor
            _mapImage.experimental.animation.Start(_mapImage.style.unityBackgroundImageTintColor.value, end, 500, (elm, val) => { elm.style.unityBackgroundImageTintColor = val; });

            _mapFaded = value;
        }
    }

    void Start()
    {
        //Get references
        _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
        _playerRepresentation = _root.Q<VisualElement>("Player");
        _mapImage = _root.Q<VisualElement>("Image");
        _mapContainer = _root.Q<VisualElement>("Map");

    }

    void LateUpdate()
    {
        //Scan for keyboard input to toggle the map
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(!IsMapOpen);
        }

        //Rotate and move the player icon based on the players movement
        var multiplyer = IsMapOpen ? fullMultiplyer : miniMultiplyer;
        _playerRepresentation.style.translate = new Translate(Player.transform.position.x * multiplyer, Player.transform.position.z * -multiplyer, 0);
        _playerRepresentation.style.rotate = new Rotate(new Angle(Player.transform.rotation.eulerAngles.y));

        //Alter the faded value if the map is open and the player is moving
        MapFaded = IsMapOpen && PlayerController.Instance.IsMoving;

        //Move the mini map 
        if (!IsMapOpen)
        {
            //Calculate the width/height bounds for the map image
            var clampWidth = _mapImage.worldBound.width / 2 - _mapContainer.worldBound.width / 2;
            var clampHeight = _mapImage.worldBound.height / 2 - _mapContainer.worldBound.height / 2;

            //Clamp the bounds so that the map doesn't scroll past the playable area (i.e. the map image)
            var xPos = Mathf.Clamp(Player.transform.position.x * -miniMultiplyer, -clampWidth, clampWidth);
            var yPos = Mathf.Clamp(Player.transform.position.z * miniMultiplyer, -clampHeight, clampHeight);

            //Move the map image
            _mapImage.style.translate = new Translate(xPos, yPos, 0);
        }
    else
    {
        //Reset it back to zero
        _mapImage.style.translate = new Translate(0, 0, 0);
    }
    }

    /// <summary>
    /// Check wither the map is in "full" mode
    /// </summary>
    private bool IsMapOpen => _root.ClassListContains("root-container-full");


    /// <summary>
    /// Toggle between full and mini mode
    /// </summary>
    /// <param name="on">Should the map be in full mode?</param>
    private void ToggleMap(bool on)
    {
        _root.EnableInClassList("root-container-mini", !on);
        _root.EnableInClassList("root-container-full", on);
    }



}
