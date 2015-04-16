using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HudEffects : MonoBehaviour
{
    Text scoreText;
    Animator anim;
    public static int score = 0;
    public static HudEffects instance;
    void Awake ()
    {
        scoreText = GetComponentInChildren<Text>();
        anim = GetComponent<Animator>();
        if (instance == null)
        {
            instance = GetComponent<HudEffects>(); 
        }
    }
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
    public void takeDamage(GameObject other)
    {
        anim.SetTrigger("takeDamage");
        score -= 20;//make this a veriable
        Destroy(other.gameObject);
    }
}
