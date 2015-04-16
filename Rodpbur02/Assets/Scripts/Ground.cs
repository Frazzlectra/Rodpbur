using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    //Fields
    public static float moveSpeed = 3;
    float timeBetweenIncrease = 10;
    float increaseMoveSpeed = .5f;
    //controlls the ground movement
    void FixedUpdate()
    {
        //timer += Time.deltaTime;
        if (GameManager.timer >= timeBetweenIncrease)
        {
            MoveSpeedTime();
        }
        gameObject.transform.position += Vector3.back * moveSpeed * Time.deltaTime;
    }

    //Destroy object when it gets out of the camera view
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroyer")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerExit(Collider other)//make new block
    {
        if (other.gameObject.tag == "mainSpawnPoint")
        {
            //Debug.Log("Spawn New");
            GameManager.instance.SpawnGround();
        }
    }
    void MoveSpeedTime()//increase movespeed and z scale of block to avoid gaps.
    {
        GameManager.timer = 0;
        moveSpeed += increaseMoveSpeed;
        gameObject.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, moveSpeed);
        //Debug.Log("moveSpeed " + moveSpeed);
        return;
    }
}
