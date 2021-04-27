using Assets.WUG.Scripts;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the health of a ship
/// </summary>
public class Health : MonoBehaviour
{
    [SerializeField, Range(0,4)]
    private int m_CurrentHealth;
    
    /// <summary>
    /// Sinks the ship by destroying the game object.
    /// In reality, you'd want a fun animation and to have it go into the ocean
    /// </summary>
    private void SinkShip()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// Reduces the ships health by 1 if it is higher than 0.
    /// Once it hits 0 the ship will be "sunk".
    /// </summary>
    public void DamageShip()
    {
        if (m_CurrentHealth > 0)
        {
            m_CurrentHealth--;
        }

        if (m_CurrentHealth == 0)
        {
            Invoke("SinkShip", 1);
        }

    }

    /// <summary>
    /// Increases the health of the ship by 1 and up to 4.
    /// Only works as long as it hasn't been sunk.
    /// </summary>
    public void HealShip()
    {
        if (m_CurrentHealth < 4)
        {
            m_CurrentHealth++;
        }
    }
}
