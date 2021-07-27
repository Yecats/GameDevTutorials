using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

public class ItemDatabase : EditorWindow
{
    private static List<Item> m_ItemDatabase = new List<Item>();

    private VisualElement m_ItemsTab;
    private static VisualTreeAsset m_ItemRowTemplate;
    private ScrollView m_DetailSection;

    private ListView m_ItemListView;
    private Sprite m_DefaultItemIcon;
    private Item m_activeItem;
    private VisualElement m_LargeDisplayIcon;

    private int m_ItemHeight = 60;

    [MenuItem("WUG/Item Database")]
    public static void Init()
    {
        ItemDatabase wnd = GetWindow<ItemDatabase>();
        wnd.titleContent = new GUIContent("Item Database");

        Vector2 size = new Vector2(1000, 475);
        wnd.minSize = size;
        wnd.maxSize = size;
    }

    public void CreateGUI()
    {
        // Import the UXML Window
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WUG/Editor/ItemDatabase.uxml");
        VisualElement rootFromUXML = visualTree.Instantiate();
        rootVisualElement.Add(rootFromUXML);

        // Import the stylesheet
        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/WUG/Editor/ItemDatabase.uss");
        rootVisualElement.styleSheets.Add(styleSheet);

        //Import the ListView Item Template
        m_ItemRowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/WUG/Editor/ItemRowTemplate.uxml");
        m_DefaultItemIcon = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/WUG/Sprites/UnknownIcon.png", typeof(Sprite));

        //Get references for later
        m_DetailSection = rootVisualElement.Q<ScrollView>("ScrollView_Details");
        m_LargeDisplayIcon = m_DetailSection.Q<VisualElement>("Icon");

        //Load all existing item assets 
        LoadAllItems();

        //Populate the listview
        m_ItemsTab = rootVisualElement.Q<VisualElement>("ItemsTab");
        GenerateListView();

        //Hook up button click events
        rootVisualElement.Q<Button>("Btn_AddItem").clicked += AddItem_OnClick;
        rootVisualElement.Q<Button>("Btn_DeleteItem").clicked += DeleteItem_OnClick;

        //Register Value Changed Callbacks for new items added to the ListView
        m_DetailSection.Q<TextField>("ItemName").RegisterValueChangedCallback(evt => 
        {
            m_activeItem.FriendlyName = evt.newValue;
            m_ItemListView.Refresh();
        });

        m_DetailSection.Q<ObjectField>("IconPicker").RegisterValueChangedCallback(evt =>
        {
            Sprite newSprite = evt.newValue as Sprite;
            m_activeItem.Icon = newSprite == null ? m_DefaultItemIcon : newSprite;
            m_LargeDisplayIcon.style.backgroundImage = newSprite == null ? m_DefaultItemIcon.texture : newSprite.texture;

            m_ItemListView.Refresh();
            
        });

    }

    /// <summary>
    /// Delete the active Item asset from the Asset/Data folder
    /// </summary>
private void DeleteItem_OnClick()
{
    //Get the path of the fie and delete it through AssetDatabase
    string path = AssetDatabase.GetAssetPath(m_activeItem);
    AssetDatabase.DeleteAsset(path);

    //Purge the reference from the list and refresh the ListView
    m_ItemDatabase.Remove(m_activeItem);
    m_ItemListView.Refresh();

    //Nothing is selected, so hide the details section
    m_DetailSection.style.visibility = Visibility.Hidden;

}

    /// <summary>
    /// Add a new Item asset to the Asset/Data folder
    /// </summary>
    private void AddItem_OnClick()
    {
        //Create an instance of the scriptable object
        Item newItem = CreateInstance<Item>();
        newItem.FriendlyName = $"New Item";
        newItem.Icon = m_DefaultItemIcon;

        //Create the asset 
        AssetDatabase.CreateAsset(newItem, $"Assets/Data/{newItem.ID}.asset");

        //Add it to the item list
        m_ItemDatabase.Add(newItem);

        //Refresh the ListView so everything is redrawn again
        m_ItemListView.Refresh();
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight + 5;

        m_ItemListView.SetSelection(m_ItemDatabase.Count - 1);
    }

    /// <summary>
    /// Look through all items located in Assets/Data and load them into memory
    /// </summary>
    private void LoadAllItems()
    {
        m_ItemDatabase.Clear();

        string[] allPaths = Directory.GetFiles("Assets/Data", "*.asset", SearchOption.AllDirectories);

        foreach (string path in allPaths)
        {
            string cleanedPath = path.Replace("\\", "/");
            m_ItemDatabase.Add((Item)AssetDatabase.LoadAssetAtPath(cleanedPath, typeof(Item)));
        }
    }   
    
    /// <summary>
    /// Create the list view based on the asset data
    /// </summary>
    private void GenerateListView()
    {
        //Defining what each item will visually look like. In this case, the makeItem function is creating a clone of the ItemRowTemplate.
        Func<VisualElement> makeItem = () => m_ItemRowTemplate.CloneTree();

        //Define the binding of each individual Item that is created. Specifically, 
        //it binds the Icon visual element to the scriptable object’s Icon property and the 
        //Name label to the FriendlyName property.
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            e.Q<VisualElement>("Icon").style.backgroundImage = m_ItemDatabase[i] == null ? m_DefaultItemIcon.texture :  m_ItemDatabase[i].Icon.texture;
            e.Q<Label>("Name").text = m_ItemDatabase[i].FriendlyName;
        };

        //Create the listview and set various properties
        m_ItemListView = new ListView(m_ItemDatabase, m_ItemHeight, makeItem, bindItem);
        m_ItemListView.selectionType = SelectionType.Single;
        m_ItemListView.style.height = m_ItemDatabase.Count * m_ItemHeight + 5;
        m_ItemsTab.Add(m_ItemListView);

        m_ItemListView.onSelectionChange += ListView_onSelectionChange;
        m_ItemListView.SetSelection(0);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="selectedItems"></param>
    private void ListView_onSelectionChange(IEnumerable<object> selectedItems)
    {
        //Get the first item in the selectedItems list. 
        //There will only ever be one because SelectionType is set to Single
        m_activeItem = (Item)selectedItems.First();

        //Create a new SerializedObject and bind the Details VE to it. 
        //This cascades the binding to the children
        SerializedObject so = new SerializedObject(m_activeItem);
        m_DetailSection.Bind(so);

        //Set the icon if it exists
        if (m_activeItem.Icon != null)
        {
            m_LargeDisplayIcon.style.backgroundImage = m_activeItem.Icon.texture;
        }

        //Make sure the detail section is visible. This can turn off when you delete an item
        m_DetailSection.style.visibility = Visibility.Visible;
    }

}
