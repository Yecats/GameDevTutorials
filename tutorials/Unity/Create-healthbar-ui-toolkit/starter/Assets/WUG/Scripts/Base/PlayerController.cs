using Assets.WUG.Scripts;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using static UnityEngine.InputSystem.InputAction;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Vector3 m_InputDirection = Vector3.zero;
    private Vector3 m_MoveTarget = Vector3.zero;
    private Rigidbody m_RigidBody;
    private float m_CurrentMovementSpeed = 0.15f;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Debug.LogError("Two instances of PlayerController detected. Deleting Gameobject.");
            Destroy(gameObject);
        }
    }

    void Start() => m_RigidBody = GetComponent<Rigidbody>();

    private void FixedUpdate()
    {
        //set forward movement vector
        m_MoveTarget = transform.forward * m_InputDirection.y;

        //move position
        m_RigidBody.MovePosition(transform.position - m_MoveTarget * m_CurrentMovementSpeed.AsPercentOfFixedDeltaTime());

        //Rotate based on keys pushed
        if (m_InputDirection.x != 0)
        {
            transform.rotation *= Quaternion.Euler(transform.rotation.eulerAngles.WithNewY(m_InputDirection.x));
        }
    }

    /// <summary>
    /// Handles WASD input and saves the input for FixedUpdate to handle
    /// </summary>
    /// <param name="context">Information on what was pushed by the player</param>
    internal void OnMove(CallbackContext context)
    {
        //Will not do anything if the window is not focused
        if (!Application.isFocused)
        {
            return;
        }

        m_InputDirection = context.canceled ? Vector3.zero : (Vector3)context.ReadValue<Vector2>();


    }

}
