using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyWater : MonoBehaviour
{
    [SerializeField] private float healthAmount;

    public float HealthAmount
    {
        get { return healthAmount; }
    }
}
