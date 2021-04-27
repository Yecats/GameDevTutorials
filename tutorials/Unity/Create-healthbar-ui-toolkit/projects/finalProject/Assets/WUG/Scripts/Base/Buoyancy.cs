using Assets.WUG.Scripts;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;


/// <summary>
/// Makes the boat look slightly more realistic by adding buoyancy.
/// Very lightweight - not a lot of time spent here so if you use it 
/// for your game you'll want to adjust it to go with the water better.
/// </summary>
public class Buoyancy : MonoBehaviour
{
    [SerializeField] private GameObject m_Model;
    [SerializeField, 
        Tooltip("Calculate Bouncy coroutine will wait for this period of " +
                 "time before checking the next wave. In Seconds.")] 
    private float m_BouncyCheckFrequency;
    public float AnimationSpeed;

    private YieldInstruction m_waitTime;

    public Vector3 TargetPosition { get; private set; }
    public Quaternion TargetRotation { get; private set; }

    private void Start()
    {
        m_waitTime = new WaitForSeconds(m_BouncyCheckFrequency);
        TargetPosition = transform.position;

        StartCoroutine(Calculate());
    }

    private void LateUpdate()
    {
        m_Model.transform.localPosition = Vector3.Lerp(m_Model.transform.localPosition, TargetPosition, Time.deltaTime * AnimationSpeed);
        m_Model.transform.localRotation = Quaternion.Slerp(m_Model.transform.localRotation, TargetRotation, Time.deltaTime * AnimationSpeed);
    }

    IEnumerator Calculate()
    {
        while (true)
        {
            TargetPosition = Random.insideUnitSphere;
            TargetRotation = Quaternion.Euler(Random.insideUnitSphere * 5f);

            yield return m_waitTime;
        }
    }
}
