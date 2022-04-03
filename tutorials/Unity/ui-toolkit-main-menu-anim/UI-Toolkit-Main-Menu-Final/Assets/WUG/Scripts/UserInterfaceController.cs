using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;
using System;

public class UserInterfaceController : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _menu;
    private bool expanded = true;
    private VisualElement _settingsButton;
    private VisualElement _cancelButton;


    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _menu = _root.Q<VisualElement>("Menu");

        var mainOptions = _menu.Q<VisualElement>("MenuContent").Children().ToList();

        _settingsButton = mainOptions[mainOptions.Count - 2];
        _cancelButton = _menu.Q<VisualElement>("SettingsContent").Children().Last();

        _settingsButton.RegisterCallback<MouseDownEvent>((evt) => 
        {
            Debug.Log("Settings Clicked");
        });

        _cancelButton.RegisterCallback<MouseDownEvent>((evt) =>
        {
            Debug.Log("Settings Clicked");
        });
    }


    // Start is called before the first frame update
    [ContextMenu("Animate")]
    public void AnimateTest()
    {
        _menu.EnableInClassList("menu-expanded", !expanded);
        _menu.EnableInClassList("menu-collapsed", expanded);

        expanded = !expanded;
    }
}
