using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Data/Item")]
public class ItemDefinition : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string FriendlyName;
    public string Description;
    public int SellPrice;
    public Sprite Icon;
    public SlotDimensions Dimensions;
}

[Serializable]
public struct SlotDimensions
{
    public int Height;
    public int Width;
}
