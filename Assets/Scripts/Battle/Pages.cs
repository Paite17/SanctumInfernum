using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pages
{
    private string pageName;
    private string pageDescription;

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
