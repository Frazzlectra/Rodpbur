using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GameObject spawnPoint;
    public GameObject ground;
    public static GameManager instance;

    void Awake()
    {
        instance = this.gameObject.GetComponent<GameManager>();
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
    }

    public void SpawnGround()
    {
        Instantiate(ground, spawnPoint.transform.position, Quaternion.identity);
    }

}
