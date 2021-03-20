using DG.Tweening;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UIElements;

public class CircularProgressBarAnimation : MonoBehaviour
{
    public static CircularProgressBarAnimation Instance;
    private VisualElement m_Root;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError("ERROR: Multiple instances of CircularProgressBarAnimation detected. Deleting Game Object");
            Destroy(this);
        }
    }

    private void Start()
    {
        //Grab a reference to the root element that is drawn
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        //Display the view of the panel
        m_Root.style.visibility = Visibility.Hidden;
    }


}
