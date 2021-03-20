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

}
