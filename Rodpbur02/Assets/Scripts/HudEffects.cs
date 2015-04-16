using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour
{
    public Text scoreText;
    public Text distanceText;    
    public static int score = 0;
    public static HudEffects instance;
    public static int health;

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
        if (timer > 10)
        {
            timer = 0;
            ++distance;
        }
        if (health <= 0)
        {
            GameManager.GameOver = true;
        }
        scoreText.text = "Score: " + score;
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
}
