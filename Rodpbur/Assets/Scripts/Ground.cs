using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{

    public static float moveSpeed = 3.5f;
    public float timeBetweenIncrease = 10;
    public float increaseMoveSpeed = .5f;

    //move blocks
    void FixedUpdate()
    {
        if (!GameManager.GameOver)
        {
            gameObject.transform.position += Vector3.back * moveSpeed * Time.deltaTime;
            //timer += Time.deltaTime;
            if (GameManager.timer >= timeBetweenIncrease)
            {
                MoveSpeedTime();
            }
        }
    }
    //destroy blocks 
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroyer")
        {
            Destroy(gameObject);
        }
    }
    //spawn blocks
    void OnTriggerExit(Collider other)//make new block
    {
        if (other.gameObject.tag == "mainSpawnPoint")
        {
            //Debug.Log("Spawn New");
            GameManager.instance.SpawnEnviroment();
        }
    }
    void MoveSpeedTime()//increase movespeed and z scale of block to avoid gaps.
    {
        GameManager.timer = 0;
        moveSpeed += increaseMoveSpeed;
        GameManager.coinDelay -= .01f; // so coins don't get farther apart as the ground speeds up
        gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, moveSpeed);
        //Debug.Log("moveSpeed " + moveSpeed);
        return;
    }
}
