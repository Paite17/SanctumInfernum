using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class HitLines : MonoBehaviour
{
    [SerializeField]
    KeyCode keyToPress;
    [SerializeField]
    Renderer rend;
    [SerializeField]
    DDRManager ddrm;
    [SerializeField]
    ParticleSystem partSys;

    bool notesReady = false;
    public float badHits, goodHits, perfectHits;
    Color pressedCol = Color.white;
    Color unPressedCol = Color.red;
    float distFromCenter { get
        {
            return Vector3.Distance(mostRecentNote.transform.position, hitCollider.transform.position);
        }
    }
    [SerializeField]
    Collider hitCollider;
    Collider mostRecentNote;

    HashSet<Collider> notesInside = new();

    private void Start()
    {
        rend = GetComponent<Renderer>();
        ddrm = GetComponent<DDRManager>();
        hitCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress) && notesReady)
        {
            foreach (Collider note in notesInside)
            {
                if (distFromCenter > 0.3 && distFromCenter < 0.6)
                {
                    Destroy(note.gameObject);
                    AddScore(1);
                    goodHits++;
                    notesReady = false;
                    GoodHit();
                    partSys.Play();

                }
                else if (distFromCenter > 0.1 && distFromCenter < 0.3)
                {
                    Destroy(note.gameObject);
                    AddScore(3);
                    perfectHits++;
                    notesReady = false;
                    PerfectHit();
                    partSys.Play();
                }

            }
            notesInside.Clear();
        }
        else if (Input.GetKeyDown(keyToPress) && !notesReady)
        {
            SubtractScore();
            badHits++;
            BadHit();
        }

        if (Input.GetKeyDown(keyToPress))
        {
            rend.material.color = pressedCol;
        }

        if (Input.GetKeyUp(keyToPress))
        {
            rend.material.color = unPressedCol;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        notesInside.Add(other);
        notesReady = true;
        mostRecentNote = other;
    }

    private void OnTriggerExit(Collider other)
    {
        notesInside.Remove(other);
        Destroy(other.gameObject);
        notesReady = false;
        SubtractScore();
    }

    void AddScore(float multiplier)
    {
        DDRManager.Instance.score += 10 * multiplier;
        DDRManager.Instance.combo += 1;
        UpdateScore();
    }

    void SubtractScore()
    {
        DDRManager.Instance.score -= 10;
        DDRManager.Instance.combo = 0;
        UpdateScore();
    }

    void UpdateScore()
    {
        DDRManager.Instance.scoreText.text = "Score: " + DDRManager.Instance.score.ToString("F0");
        DDRManager.Instance.comboText.text = "Combo: " + DDRManager.Instance.combo.ToString("F0") + "x";
    }

    void BadHit()
    {
        DDRManager.Instance.badHitText.CrossFadeAlpha(1f, 0.0001f, false);
        DDRManager.Instance.badHitText.text = "Bad!";
        DDRManager.Instance.badHitText.CrossFadeAlpha(0f, 1.5f, false);

    }

    void GoodHit()
    {
        DDRManager.Instance.goodHitText.CrossFadeAlpha(1f, 0.0001f, false);
        DDRManager.Instance.goodHitText.text = "Good!";
        DDRManager.Instance.goodHitText.CrossFadeAlpha(0f, 1.5f, false);
    }

    void PerfectHit()
    {
        DDRManager.Instance.perfectHitText.CrossFadeAlpha(1f, 0.0001f, false);
        DDRManager.Instance.perfectHitText.text = "Perfect!";
        DDRManager.Instance.perfectHitText.CrossFadeAlpha(0f, 1.5f, false);

    }

}
