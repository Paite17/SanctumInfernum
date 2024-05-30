using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroCutsceneTimer : MonoBehaviour
{
    public Slider loadingProgress;
    public GameObject loadingScreen;
    [SerializeField] private float cutsceneTimer = 55f;

    void Update()
    {
        cutsceneTimer -= Time.deltaTime;

        if(cutsceneTimer <= 0 || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LoadingManager("Slums")); 
            //SceneManager.LoadScene("Dungeon w Castle");
        }
    }

    // copy + pasted from main menu (would be nice to make a specific system instead but oh well
    private IEnumerator LoadingManager(string scene)
    {
        Debug.Log("Start loading");
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(scene);
        loadingScreen.SetActive(true);
        while (!loadOperation.isDone)
        {
            // loading bar
            float progress = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingProgress.value = progress;

            yield return null;
        }
    }
}
