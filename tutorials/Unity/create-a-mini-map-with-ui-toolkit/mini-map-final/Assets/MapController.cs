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
    private bool IsMapOpen => _root.style.visibility == Visibility.Visible;
    private bool _mapFaded;
    public bool MapFaded
    {
        get => _mapFaded; 
        
        set
        {
            _mapFaded = value;
            _mapImage.experimental.animation.Start(_mapImage.style.unityBackgroundImageTintColor.value, value 
                ? _mapImage.style.unityBackgroundImageTintColor.value.WithAlpha(.5f) 
                : Color.white, 1000, (elm, val) => { elm.style.unityBackgroundImageTintColor = val; });
        }
    }

    private void ToggleMap(bool on) => _root.style.visibility = on ? Visibility.Visible : _root.style.visibility = Visibility.Hidden;

    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _playerRepresentation = _root.Q<VisualElement>("Player");
        _mapImage = _root.Q<VisualElement>("Image");

        ToggleMap(false);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ToggleMap(!IsMapOpen);
        }

        _playerRepresentation.transform.position = new Vector3(Player.transform.position.x * Multiplyer, Player.transform.position.z * -Multiplyer, 0);

        //Animate the fade of the map
        if (!MapFaded && PlayerController.Instance.IsMoving && IsMapOpen)
        {
            MapFaded = true;    
        }
        else if (MapFaded && !PlayerController.Instance.IsMoving && IsMapOpen)
        {
            MapFaded = false;
        }
    }
    
}
