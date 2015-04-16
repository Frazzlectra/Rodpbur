using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GameObject spawnPoint;
    GameObject[] coinSpawnPoints;
    public GameObject ground;
    public static GameManager instance;
    private Transform groundClones;
    GameObject newGround;
    int rnd;//for spawning obsticles etc.
    //obsticles
    public GameObject deathBlock;
    GameObject newDeathBlock;
    public GameObject coin;
    GameObject newCoin;
    bool spawningCoins = false;

    void Awake()
    {
        instance = this.gameObject.GetComponent<GameManager>();        
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
        coinSpawnPoints = GameObject.FindGameObjectsWithTag("coinSpawn");
        Debug.Log("Coin spawn points: " + coinSpawnPoints[0] + " " + coinSpawnPoints[1]);
        groundClones = new GameObject("GroundClones").transform;
    }
    //spawns new ground when spawn trigger is empty
    public void SpawnGround()
    {
        rnd = Random.Range(0, 3);
        newGround = Instantiate(ground, spawnPoint.transform.position,Quaternion.identity ) as GameObject;
        newGround.transform.SetParent(groundClones);
        if (rnd % 2 == 0 && !spawningCoins)
        {
            rnd = Random.Range(10, 15);
            StartCoroutine(SpawnCoins(rnd));
            spawningCoins = true;
        }
        if (rnd % 2 == 1)
        {
            SpawnDeathBlocks();
        }
    }
    //spawn a rnd number of coins 
    IEnumerator SpawnCoins(int numToSpawn)
    {        
        int numSpawned = 0;
        
        while (numSpawned < numToSpawn)
        {
            rnd = Random.Range(0, 2);
            Debug.Log(rnd);
            newCoin = Instantiate(coin, coinSpawnPoints[rnd].transform.position, Quaternion.identity) as GameObject;//screwed shit up but not anymore
            newCoin.transform.SetParent(newGround.transform);
            ++numSpawned;
            yield return new WaitForSeconds(1);
        }
        if (numSpawned >= numToSpawn)
        {
            spawningCoins = false;
        }
    }
    
    void SpawnDeathBlocks()
    {
        rnd = Random.Range(0, 1);
        //spawn new death block from coin spawnpoint
        newDeathBlock = Instantiate(deathBlock, coinSpawnPoints[rnd].transform.position, Quaternion.identity) as GameObject;
        newDeathBlock.transform.SetParent(newGround.transform);
    }
}
