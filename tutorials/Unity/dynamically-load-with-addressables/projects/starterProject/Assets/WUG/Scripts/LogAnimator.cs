using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.WUG.Scripts;
public class LogAnimator : MonoBehaviour
{
    public Transform m_Model => transform.GetChild(0);

    public const float k_TotalTime = 30f;
    private float m_RemainingTime = 0f;
    private float m_Time = 0f;

    private float m_BobAmount = 0;
    private Vector3 m_startPosition;
    private Vector3 m_EndPosition;

    void Start()
    {
        m_BobAmount = Random.Range(0.75f, 1.25f);
        m_startPosition = transform.position;
        m_EndPosition = transform.position.WithNewZ(-44f);
        m_RemainingTime = (Vector3.Distance(m_startPosition, m_EndPosition) / 86f) * k_TotalTime;
        m_Model.transform.localRotation = Quaternion.Euler(m_Model.transform.localRotation.eulerAngles.WithNewZ(Random.Range(0, 360)));

    }

    private void Update()
    {
        m_Time += Time.deltaTime / m_RemainingTime;
        transform.position = Vector3.Lerp(m_startPosition, m_EndPosition, m_Time);

        m_Model.localPosition = Vector3.up * (Mathf.PingPong(Time.time, m_BobAmount) - .5f);
    }

}
