using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GameObject spawnPoint;
    GameObject coinSpawnPoint;
    public GameObject ground;
    public static GameManager instance;
    private Transform groundClones;
    GameObject newGround;
    int rnd;//for spawning obsticles etc.
    public GameObject coin;
    GameObject newCoin;

    void Awake()
    {
        instance = this.gameObject.GetComponent<GameManager>();        
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
        coinSpawnPoint = GameObject.FindGameObjectWithTag("coinSpawn");
        groundClones = new GameObject("GroundClones").transform;
    }

    public void SpawnGround()
    {
        rnd = Random.Range(0, 2);
        newGround = Instantiate(ground, spawnPoint.transform.position,Quaternion.identity ) as GameObject;
        newGround.transform.SetParent(groundClones);
        if (rnd % 2 == 0)
        {
            newCoin = Instantiate(coin, coinSpawnPoint.transform.position, Quaternion.identity) as GameObject;//screws shit up
            newCoin.transform.SetParent(newGround.transform);
        }
    }
}
