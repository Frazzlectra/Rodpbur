using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ground : MonoBehaviour
{
    //Fields
    float moveSpeed = 3;

    //controlls the ground movement
    void FixedUpdate()
    {
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
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "mainSpawnPoint")
        {
            Debug.Log("Spawn New");
            GameManager.instance.SpawnGround();
        }
    }
}
