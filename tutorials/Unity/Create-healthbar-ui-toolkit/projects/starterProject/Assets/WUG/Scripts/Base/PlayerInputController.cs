using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using UnityEngine;

namespace Assets.WUG.Scripts
{
    /// <summary>
    /// Maps Input events to actions
    /// </summary>
    public class PlayerInputController : MonoBehaviour
    {

        private InputActions m_InputMapping;

        private void Awake() => m_InputMapping = new InputActions();

        private void Start()
        {
            m_InputMapping.Player.Move.started += PlayerController.Instance.OnMove;
            m_InputMapping.Player.Move.performed += PlayerController.Instance.OnMove;
            m_InputMapping.Player.Move.canceled += PlayerController.Instance.OnMove;
        }

        private void OnEnable() => m_InputMapping.Enable();

        private void OnDisable() => m_InputMapping.Disable();

    }
}
