using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class DDRManager : MonoBehaviour
{
    public static DDRManager Instance {  get; private set; }

    [SerializeField]
    Transform spawn1, spawn2, spawn3, spawn4;
    [SerializeField]
    Collider hit1, hit2, hit3, hit4;
    [SerializeField]
    float instancePause = 0.7f;
    public float score, combo, finalScore;
    public TextMeshProUGUI scoreText, comboText, badHitText, goodHitText, perfectHitText, finalScoreText;
    HitLines hitLines;

    [SerializeField]
    GameObject[] cubesToSpawn;
    [SerializeField]
    GameObject[] spawnPositions;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {   
        hitLines = GetComponent<HitLines>();
        StartCoroutine(MusicSequence());
    }

    private void Update()
    {
        
    }

    IEnumerator MusicSequence()
    {
        for (int i = 0; i < cubesToSpawn.Length; i++)
        {
            SpawnCube(cubesToSpawn[i], spawnPositions[i].transform.position);

            if (i < cubesToSpawn.Length)
            {
                yield return new WaitForSeconds(instancePause);
            }
            else { break; }
        }
        FinalScore();

        //yield return new WaitForSeconds(2);
        //rhythmCube1 = Instantiate(rhythmCube1, OSpawn1, gameObject.transform.rotation);

        //yield return new WaitForSeconds(instancePause);
        //rhythmCube2 = Instantiate(rhythmCube2, OSpawn2, gameObject.transform.rotation);

        //yield return new WaitForSeconds(instancePause);
        //rhythmCube3 = Instantiate(rhythmCube3, OSpawn3, gameObject.transform.rotation);

        //yield return new WaitForSeconds(instancePause);
        //rhythmCube4 = Instantiate(rhythmCube4, OSpawn4, gameObject.transform.rotation);

    }

    void SpawnCube(GameObject cubeToSpawn, Vector3 posToSpawnAt)
    {
        Instantiate(cubeToSpawn, posToSpawnAt, Quaternion.identity);
    }

    //private void MoveRhythmCube(GameObject cubeToMove, Transform endPos)
    //{
    //    Vector3 cubeV = cubeToMove.transform.position;
    //    Vector3 cubeEndV = endPos.transform.position;
    //    cubeToMove.transform.position = Vector3.MoveTowards(cubeV, cubeEndV, fallSpeed);
    //}

    public void GrowText()
    {
        StartCoroutine(GrowTextCR());
    }

    public IEnumerator GrowTextCR()
    {
        float duration = 0.5f;
        float currentTime = 0f;
        while (currentTime < duration)
        {
            float size = Mathf.Lerp(65f, 75f, currentTime / duration);
            badHitText.fontSize = size;
            currentTime += Time.deltaTime;
            yield return null;
        }
        yield break;
    }

    public void FinalScore()
    {
        float comboScore = combo * 10;
        float accuracyScore = score - (hitLines.badHits * 25) + (hitLines.goodHits * 25) + (hitLines.perfectHits * 50);
        finalScore = score + comboScore;

        finalScoreText.text = "Final Score: " + finalScore.ToString("F0");
        finalScoreText.CrossFadeAlpha(1f, 2f, false);
    }
}