using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[Serializable]
public class ItemDetails
{
    public string Name;
    public string GUID;
    public Sprite Icon;
    public bool CanDrop;
}

public enum InventoryChangeType
{
    Pickup,
    Drop
}
public delegate void OnInventoryChangedDelegate(string[] itemGuid, InventoryChangeType change);

/// <summary>
/// Generates and controls access to the Item Database and Inventory Data
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField]
    public List<Sprite> IconSprites;
    private static Dictionary<string, ItemDetails> m_ItemDatabase = new Dictionary<string, ItemDetails>();
    private List<ItemDetails> m_PlayerInventory = new List<ItemDetails>();
    public static event OnInventoryChangedDelegate OnInventoryChanged = delegate { };


    private void Awake()
    {
        PopulateDatabase();
    }

    private void Start()
    {
        //Add the ItemDatabase to the players inventory and let the UI know that some items have been picked up
        m_PlayerInventory.AddRange(m_ItemDatabase.Values);
        OnInventoryChanged.Invoke(m_PlayerInventory.Select(x=> x.GUID).ToArray(), InventoryChangeType.Pickup);
    }

    /// <summary>
    /// Populate the database
    /// </summary>
    public void PopulateDatabase()
    {
        m_ItemDatabase.Add("8B0EF21A-F2D9-4E6F-8B79-031CA9E202BA", new ItemDetails()
        {
            Name = "History of the Syndicate: 1501 to 1825 ",
            GUID = "8B0EF21A-F2D9-4E6F-8B79-031CA9E202BA",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("syndicate")),
            CanDrop = false
        });

        m_ItemDatabase.Add("992D3386-B743-4CD3-9BB7-0234A057C265", new ItemDetails()
        {
            Name = "Health Potion",
            GUID = "992D3386-B743-4CD3-9BB7-0234A057C265",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("potion")),
            CanDrop = true
        });

        m_ItemDatabase.Add("1B9C6CAA-754E-412D-91BF-37F22C9A0E7B", new ItemDetails()
        {
            Name = "Bottle of Poison",
            GUID = "1B9C6CAA-754E-412D-91BF-37F22C9A0E7B",
            Icon = IconSprites.FirstOrDefault(x => x.name.Equals("poison")),
            CanDrop = true
        });

    }

    /// <summary>
    /// Retrieve item details based on the GUID
    /// </summary>
    /// <param name="guid">ID to look up</param>
    /// <returns>Item details</returns>
    public static ItemDetails GetItemByGuid(string guid)
    {
        if (m_ItemDatabase.ContainsKey(guid))
        {
            return m_ItemDatabase[guid];
        }

        return null;
    }

}
