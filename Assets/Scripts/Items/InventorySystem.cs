using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static InventorySystem Instance;

    //Creates a list of keys to be used
    public List<Keys> KeyItems = new List<Keys>();

    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Is for the keys to be picked up by player
    public enum Keys
    {
        Key1,
        Key2,
        Key3,
        Key4,
        Key5,
        Key6,
        Key7,
        Key8
    }

    //Add key to the Keys list
    public void AddKey(Keys key)
    {
        if(!KeyItems.Contains(key))
        {
            KeyItems.Add(key);
        }
    }

    //Removes key from the Keys list
    public void RemoveKey(Keys key)
    {
        if (KeyItems.Contains(key))
        {
            KeyItems.Remove(key);
        }
    }
}
