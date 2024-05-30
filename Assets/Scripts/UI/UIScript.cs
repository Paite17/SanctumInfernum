using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIScript : MonoBehaviour
{
    [SerializeField] private Image bossHealthBar;
    [SerializeField] private Image bossHealthBarBackground;
    [SerializeField] private EnemyMeleeHealth bossHealth;
    [SerializeField] private GameObject bossUI;
    [SerializeField] private GameObject hurtOverlay;
    [SerializeField] private GameObject interactPrompt;
    private float maxBossHealth;

    private float bossHealthPercentage;

    private UIHealthBar playerHealth;

    private PlayerMovement plrMovement;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Slums")
        {
            bossHealth = GameObject.Find("SlumsBoss").GetComponent<EnemyMeleeHealth>();
            maxBossHealth = bossHealth.enemyAgentHealth;
        }
        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            maxBossHealth = bossHealth.enemyAgentHealth;
        }

        playerHealth = FindObjectOfType<UIHealthBar>();
        plrMovement = FindObjectOfType<PlayerMovement>();
    }


    // Update is called once per frame
    void Update()
    {
        if (bossHealth != null) 
        {
            bossHealthPercentage = Mathf.Round((100 * bossHealth.enemyAgentHealth) / maxBossHealth) / 100;
            bossHealthBar.fillAmount = bossHealthPercentage;
        }
        

        // colour change
        if (bossHealthPercentage <= 0.5f && bossHealthPercentage > 0.25f)
        {
            bossHealthBar.color = new Color32(255, 90, 0, 255);
            bossHealthBarBackground.color = new Color32(128, 64, 34, 255);
        }
        if (bossHealthPercentage <= 0.25f)
        {
            bossHealthBar.color = Color.red;
            bossHealthBarBackground.color = new Color32(128, 38, 34, 255);
        }

        if (bossHealth.enemyAgentHealth <= 0)
        {
            bossUI.SetActive(false);
        }

        if (playerHealth.health <= 50)
        {
            hurtOverlay.SetActive(true);
        }
        else
        {
            hurtOverlay.SetActive(false);
        }

        if (playerHealth.health <= 0)
        {
            hurtOverlay.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (plrMovement.onDialogue || plrMovement.onPickup)
        {
            interactPrompt.SetActive(true);
        }
        else
        {
            interactPrompt.SetActive(false);
        }
    }

    public void ShowBossHealthBar()
    {
        bossUI.SetActive(true);
    }

    // on game over
    public void RespawnButton()
    {
        switch (SceneManager.GetActiveScene().name)
        {
            case "Level 1":
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("Level 1");
                break;
            case "Slums":
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("Slums");
                break;
            case "EndSlums":
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("EndSlums");
                break;
        }
    }

    public void GameOverQuitButton()
    {
        GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("Slums");
    }
}
