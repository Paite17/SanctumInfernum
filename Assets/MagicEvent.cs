using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEvent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AkSoundEngine.PostEvent("Magic_Projectile", gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
