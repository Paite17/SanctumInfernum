using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBase : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int itemID;
    [SerializeField][TextArea] private string itemDescription;
    [SerializeField] private Sprite itemIcon;

    // 3D model ref???????

    public string ItemName
    {
        get { return itemName; }
    }

    public int ItemID
    {
        get { return itemID; }
    }

    public string ItemDescription
    {
        get { return itemDescription; }
    }

    public Sprite ItemIcon
    {
        get { return itemIcon; }
    }
}
