using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePages : ScriptableObject
{
    [SerializeField] private string pageName;
    [SerializeField][TextArea] private string pageDescription;

    // rhythm data to be stored here

    public string PageName
    {
        get { return pageName; }
    }

    public string PageDescription
    {
        get { return pageDescription; }
    }
}
