using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private Animator m_AnimatorController;

    private void Start()
    {
        m_AnimatorController = GetComponent<Animator>();
        Invoke("AnimateDrumming", 3f);
    }

    private void AnimateDrumming()
    {
        m_AnimatorController.SetTrigger("Drum");
    }

    private void AnimateProgressBar()
    {
        //Gets a reference to the "Drum" clip on the controller
        AnimationClip clip = m_AnimatorController.runtimeAnimatorController.animationClips.Where(x => x.name.Equals("drumming")).FirstOrDefault();

        //Call the method that controls the circular animations
        //If the clip was not found, the animation will default to 5f (Which will look weird so hopefully it was found. Better practice would be to throw an error.)
        CircularProgressBarAnimation.Instance.AnimateCircularProgressBar(clip == null ? 5f: clip.length);
    }
}
