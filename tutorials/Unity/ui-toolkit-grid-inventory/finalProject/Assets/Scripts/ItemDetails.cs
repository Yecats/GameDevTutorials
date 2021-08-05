using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemDetails : MonoBehaviour
{
    [SerializeField]
    private ItemDefinition m_ItemInfo;
    private VisualElement m_Root;

    private void Start()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;

        m_Root.Q<VisualElement>("Container").style.backgroundImage = m_ItemInfo.Icon.texture;
    }

    public ItemDefinition GetItemDetails()
    {
        return m_ItemInfo;
    }

}
