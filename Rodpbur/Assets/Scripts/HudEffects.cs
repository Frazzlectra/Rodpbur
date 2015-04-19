using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour
{
    //buttons
    public Button playBtn;
    public Button quitBtn;
    //score && Hud
    public Text scoreText;
    public Text distanceText;
    public Text distanceHighText;
    public static int score = 0;
    public static HudEffects instance;
    int startingHealth = 200;
    public static int health;

    Animator anim;
    int distance = 0;
    private float timer;
    private static int distanceHighScore = 0;
    void Awake ()
    {
        health = startingHealth;
        anim = GetComponent<Animator>();
        if (instance == null)
        {
            instance = GetComponent<HudEffects>(); 
        }
        playBtn.onClick.AddListener(() => { ButtonClicked("play"); });
        quitBtn.onClick.AddListener(() => { ButtonClicked("quit"); });
    }

    private void ButtonClicked(string btn)
    {
        score = 0;
        distance = 0;
        health = startingHealth;
        GameManager.GameOver = false;
        GameManager.coinDelay = 1;
        Ground.moveSpeed = 3.5f;
        switch (btn)
        {
            case"play":
                Application.LoadLevel(1);
                break;
            case"quit":
                Application.LoadLevel(0);
                break;
            default:
                Debug.Log("Error in Button Clicked Hud Effects");
                break;
        }
    }

    void Update()
    {
        timer += Ground.moveSpeed  * Time.deltaTime;
        if (timer > 10 && !GameManager.GameOver)
        {
            timer = 0;
            ++distance;
        }
        if (health <= 0 && !GameManager.GameOver)
        {
            GameManager.GameOver = true;
            StartCoroutine(EndGame());
        }
        scoreText.text = "Gold: " + score;
        distanceText.text = "Distance: " + distance;
        if (GameManager.GameOver && distance > distanceHighScore)
        {
            distanceHighScore = distance;
        }

        distanceHighText.text = "Greatest Distance: " + distanceHighScore;
    }

    public void takeDamage(GameObject other, int damage)
    {
        anim.SetTrigger("takeDamage");
        Destroy(other.gameObject);
        if (damage > 20)
        {
            health -= damage;
            return;
        }
        score -= damage;//make this a veriable
        if (score <= 0)
        {
            score = 0;
        }
    }
    IEnumerator EndGame()
    {
        anim.SetBool("gameOver", true);
        yield return new WaitForSeconds(.13f);
        anim.SetBool("gameOver", false);
    }
}
