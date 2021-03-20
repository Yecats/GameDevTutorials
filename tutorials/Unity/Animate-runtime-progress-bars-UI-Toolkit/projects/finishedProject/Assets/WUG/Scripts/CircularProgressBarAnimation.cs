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

    private Label m_PercentageText;
    private VisualElement m_OuterPivot;
    private VisualElement m_InnerPivot;

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

        //Grab progress bar references
        m_PercentageText = m_Root.Q<Label>("txt_Percentage");
        m_OuterPivot = m_Root.Q<VisualElement>("Outer_Pivot");
        m_InnerPivot = m_Root.Q<VisualElement>("Inner_Pivot");

        //Display the view of the panel
        m_Root.style.visibility = Visibility.Hidden;
    }

    public void AnimateCircularProgressBar(float duration)
    {
        StartCoroutine(AnimateUI(duration));
    }


    private IEnumerator AnimateUI(float duration)
    {
        //Reset the text
        m_PercentageText.text = "1%";

        //Toggle visibility on
        m_Root.style.visibility = Visibility.Visible;

        //Set the tweens
        Tween outerTween = DOTween.To(() => m_OuterPivot.worldTransform.rotation.eulerAngles, x => m_OuterPivot.transform.rotation = Quaternion.Euler(x), new Vector3(0, 0, 360), 5 / 0.5f).SetEase(Ease.Linear).SetLoops(-1);
        Tween innerTween = DOTween.To(() => m_InnerPivot.worldTransform.rotation.eulerAngles, x => m_InnerPivot.transform.rotation = Quaternion.Euler(x), new Vector3(0, 0, -360), duration / 0.5f).SetEase(Ease.Linear).SetLoops(-1);
        DOTween.To(() => 5, x => m_PercentageText.text = $"{x}%", 100, duration).SetEase(Ease.Linear).OnComplete(() => { outerTween.Kill(); innerTween.Kill(); });

        //Wait until tweens finish (+1 extra second for display purposes) 
        yield return new WaitForSeconds(duration + 1f);

        //Disable the visiblity
        m_Root.style.visibility = Visibility.Hidden;

    }

}
