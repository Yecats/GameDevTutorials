using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Temp : MonoBehaviour
{
    private VisualElement _root;
    private VisualElement _menu;

    private void Start()
    {
        _root = GetComponent<UIDocument>().rootVisualElement;
        _menu = _root.Q<VisualElement>("Menu");
    }

    // Start is called before the first frame update
    [ContextMenu("Animate")]
    public void AnimateTest()
    {
        _menu.style.left = new StyleLength(-200f);
    }
}
