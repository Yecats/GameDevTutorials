using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UIElements.Experimental;

public class MapController : MonoBehaviour
{
    public GameObject Player;
    private VisualElement _root;
    private VisualElement _playerRepresentation;
    private VisualElement _mapContainer;
    private VisualElement _mapImage;

    [Range(1,15)]
    public float Multiplyer = 1f;
    private bool IsMapOpen => _root.ClassListContains("root-container-full");
    private bool _mapFaded;
    public bool MapFaded
    {
        get => _mapFaded; 
        
        set
        {
            _mapFaded = value;

            _mapImage.experimental.animation.Start(_mapImage.style.unityBackgroundImageTintColor.value, value 
                ? _mapImage.style.unityBackgroundImageTintColor.value.WithAlpha(.5f) 
                : Color.white, 500, (elm, val) => { elm.style.unityBackgroundImageTintColor = val; });
        }
    }

    private void ToggleMap(bool on)
    {
        _root.EnableInClassList("root-container-mini", !on);
        _root.EnableInClassList("root-container-full", on);
    }

    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("container");
        _playerRepresentation = _root.Q<VisualElement>("Player");
        _mapImage = _root.Q<VisualElement>("Image");
        _mapContainer = _root.Q<VisualElement>("Map");

        //strange bug that fixes dimming properly the first time
        _mapImage.style.unityBackgroundImageTintColor = Color.white;

    }

    void LateUpdate()
    {
        //Scan for keyboard input to toggle the map
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(!IsMapOpen);
        }

        //Rotate and move the player icon based on the players movement
        _playerRepresentation.style.translate = new Translate(Player.transform.position.x * Multiplyer, Player.transform.position.z * -Multiplyer, 0);
        _playerRepresentation.style.rotate = new Rotate(new Angle(Player.transform.rotation.eulerAngles.y));

        //Animate the fade of the map when open
        if (!MapFaded && PlayerController.Instance.IsMoving && IsMapOpen)
        {
            MapFaded = true;    
        }
        else if (MapFaded && !PlayerController.Instance.IsMoving && IsMapOpen)
        {
            MapFaded = false;
        }

        //Move the mini map 
        if (!IsMapOpen)
        {
            //Calculate the width/height bounds for the map image
            var clampWidth = _mapImage.worldBound.width / 2 - _mapContainer.worldBound.width / 2;
            var clampHeight = _mapImage.worldBound.height / 2 - _mapContainer.worldBound.height / 2;

            //Clamp the bounds so that the map doesn't scroll past the playable area (i.e. the map image)
            var xPos = Mathf.Clamp(Player.transform.position.x * -Multiplyer, -clampWidth, clampWidth);
            var yPos = Mathf.Clamp(Player.transform.position.z * Multiplyer, -clampHeight, clampHeight);

            //Move the map image
            _mapImage.style.translate = new Translate(xPos, yPos, 0);

        }
    }
    
}
