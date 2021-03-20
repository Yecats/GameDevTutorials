using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class LoadingProgressBarAnimation : MonoBehaviour
{
    private VisualElement m_Root;
    
    private VisualElement m_LoadingProgressBar;
    private Label m_LoadingPercentageText;

    // Start is called before the first frame update
    void Start()
    {
        //Grab a reference to the root element that is drawn
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        //Search the root for the two dynamic elements that need to be animated
        m_LoadingProgressBar = m_Root.Q<VisualElement>("bar_Progress");
        m_LoadingPercentageText = m_Root.Q<Label>("txt_Percentage");

        //Wait 2 seconds before starting the animation
        Invoke("AnimateLoadingBar", 2f);

    }

    private void AnimateLoadingBar()
    {
        //Grab the final width of the progress bar based on the parent
        //Removing 25px to account for margins
        float endWidth = m_LoadingProgressBar.parent.worldBound.width - 25;

        //Set the tweens
        DOTween.To(() => 5, x=> m_LoadingPercentageText.text = $"{x}%", 100, 5f).SetEase(Ease.Linear);
        DOTween.To(() => m_LoadingProgressBar.worldBound.width, x => m_LoadingProgressBar.style.width = x, endWidth, 5f).SetEase(Ease.Linear);
    }


    
}
