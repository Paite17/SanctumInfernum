using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    // Is used to check if the key required to open a door is in the player's inventory
    [SerializeField] InventorySystem.Keys correctKey;
    public GameObject ThisDoor;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Checks if the player has the correct/required key or not 
    public bool HasCorrectKey(InventorySystem.Keys requiredKey)
    {
        if (InventorySystem.Instance.KeyItems.Contains(requiredKey))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }

    //If the player has the correct key, the door will go away
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (HasCorrectKey(correctKey))
            {
                anim.SetBool("DoorIsOpening", true);
                
            }
            else
            {
                Debug.Log("You don't have the right key");
            }
        }
    }
}
