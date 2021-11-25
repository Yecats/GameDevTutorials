using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapController : MonoBehaviour
{
    public GameObject Player;
    private VisualElement _root;
    private VisualElement _playerRepresentation;

    public float Multiplyer = 1f;


    // Start is called before the first frame update
    void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _playerRepresentation = _root.Q<VisualElement>("Player");

        Debug.Log($"Player: {Player.transform.position}");
        Debug.Log($"Player VE: {_playerRepresentation.worldBound.position}");

    }

    // Update is called once per frame
    void LateUpdate()
    {
        _playerRepresentation.transform.position = new Vector3(Player.transform.position.x * Multiplyer, Player.transform.position.z * -Multiplyer, 0);
        Debug.Log($"Player VE: {_playerRepresentation.worldBound.position}");
    }
}
