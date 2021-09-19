using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Data/Item")]
public class ItemDefinition : ScriptableObject
{
    public string ID = Guid.NewGuid().ToString();
    public string FriendlyName;
    public string Description;
    public int SellPrice;
    public Sprite Icon;
    public Dimensions SlotDimension;
}

[Serializable]
public struct Dimensions
{
    public int Height;
    public int Width;
}
