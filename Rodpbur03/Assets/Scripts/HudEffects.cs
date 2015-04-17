using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour
{
    public Text scoreText;
    public Text distanceText;    
    public static int score = 0;
    public static HudEffects instance;
    public static int health = 200;

    Animator anim;
    int distance = 0;
    private float timer;

    void Awake ()
    {        
        anim = GetComponent<Animator>();
        if (instance == null)
        {
            instance = GetComponent<HudEffects>(); 
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
    }
    IEnumerator EndGame()
    {
        anim.SetBool("gameOver", true);
        yield return new WaitForSeconds(.13f);
        anim.SetBool("gameOver", false);
    }
}
