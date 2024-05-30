using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider easeHealthSlider;
    public float maxHealth = 200f;
    public float health;
    private float lerpSpeed = 0.05f;

    // hard coding because i'm cool :sunglasses:
    public bool isProbablyDyingToSlumsBoss;

    // i'm lazy
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject gameOverBackground;
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject gameOverButtons;



    // you have not experienced the hell i experienced before implementing this variable
    private bool preventHell;
    private bool preventHell2;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
        easeHealthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

        healthSlider.value = health;
        /*if (Input.GetKeyDown(KeyCode.Space)) 
        {
            takeDamage(10);
        } */


        // this does nothing btw
        if (healthSlider.value != easeHealthSlider.value) 
        {
            easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, health, lerpSpeed);
        }

        if (health <= 0)
        {
            
            Debug.Log("uh oh stinky");
            //Destroy(gameObject);
            StartCoroutine(DeathSequence());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EnemyProjectile")
        {
            health -= 20;
        }
        else if (collision.gameObject.tag == "HealthItem")
        {
            health = 100;
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "BossAttack":
                isProbablyDyingToSlumsBoss = true;
                break;
            case "HealthItem":
                Heal(other.GetComponent<HolyWater>().HealthAmount);
                Destroy(other.gameObject);
                break;
        }
    }


    public void TakeDamage(float damage)
    {
        health -= damage;
        AkSoundEngine.PostEvent("Player_Injured", gameObject);
    }

    public void Heal(float healthReturn)
    {
        health += healthReturn;
    }

    private IEnumerator DeathSequence()
    {
        
        GetComponent<PlayerMovement>().canMove = false;
        bool scripted = false;
        if (SceneManager.GetActiveScene().name == "Slums" && isProbablyDyingToSlumsBoss == true)
        {
            scripted = true;
        }
        else
        {
            scripted = false;
        }

        // play death animation (i hope)
        anim.SetBool("Death", true);

        yield return new WaitForSeconds(0.3f);

        anim.SetBool("IsDying", true);

        yield return new WaitForSeconds(5);

        // set fade in
        gameOverBackground.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        if (scripted)
        {
            if (!preventHell)
            {
                GameObject.Find("LoadingManager").GetComponent<LoadingManager>().StartLoadingArea("CutsceneDialogue");
                preventHell = true;
            }
        }
        else
        {
            gameOverText.SetActive(true);
        }

        yield return new WaitForSeconds(2.5f);

        gameOverButtons.SetActive(true);
        
        
    }

}
