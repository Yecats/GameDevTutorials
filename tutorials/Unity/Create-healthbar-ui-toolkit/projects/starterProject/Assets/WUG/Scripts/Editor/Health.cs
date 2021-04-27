using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Health))]
public class HealthInspector : Editor
{
    private Health m_Health;

    private void OnEnable()
    {
        m_Health = target as Health;

        EditorApplication.update += RedrawView;
    }

    void RedrawView()
    {
        Repaint();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Damage Ship"))
        {
            m_Health.DamageShip();
        }

        if (GUILayout.Button("Heal Ship"))
        {
            m_Health.HealShip();
        }
    }

}
