using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePooPoo : MonoBehaviour
{
    public void EndCutscene()
    {
        StartCoroutine(EndingTheCutsceneWithTimer());
    }

    private IEnumerator EndingTheCutsceneWithTimer()
    {

        yield return new WaitForSeconds(2.5f);
        Debug.LogWarning("HELPME");
        GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("Level 1");
    }

}
