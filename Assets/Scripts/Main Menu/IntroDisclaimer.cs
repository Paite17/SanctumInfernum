using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDisclaimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IntroDisclaimerMoment());
    }

    private IEnumerator IntroDisclaimerMoment()
    {
        yield return new WaitForSeconds(3);

        SceneManager.LoadScene("MainMenu");
    }
}
