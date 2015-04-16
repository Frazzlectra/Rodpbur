using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public GameObject ground;
    public static GameManager instance;
    int rnd;//for spawning obsticles etc.
    //obsticles
    public GameObject deathBlock;
    public GameObject coin;
    public GameObject berrier;
    //Ground speed increase
    public static float timer;
    public static bool GameOver = false;


    GameObject spawnPoint;
    GameObject[] coinSpawnPoints;
    //obsticle instantiation
    GameObject newCoin;
    GameObject newDeathBlock;
    GameObject newBerrier;
    bool spawningCoins = false;
    GameObject newGround;
    int spaceBetweenBerriers;
    private Transform groundClones;


    void Awake()
    {
        instance = this.gameObject.GetComponent<GameManager>();        
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
        coinSpawnPoints = GameObject.FindGameObjectsWithTag("coinSpawn");
        //Debug.Log("Coin spawn points: " + coinSpawnPoints[0] + " " + coinSpawnPoints[1]);
        groundClones = new GameObject("GroundClones").transform;
    }
    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
    //spawns new ground when spawn trigger is empty
    public void SpawnGround()
    {
        rnd = Random.Range(0, 4);
        newGround = Instantiate(ground, spawnPoint.transform.position, Quaternion.identity ) as GameObject;
        newGround.transform.SetParent(groundClones);
        ++spaceBetweenBerriers;
        if (rnd == 0 && !spawningCoins)
        {
            rnd = Random.Range(10, 15);
            StartCoroutine(SpawnCoins(rnd));
            spawningCoins = true;
        }
        if (rnd == 1)
        {
            SpawnDeathBlocks();
        }
        if (rnd == 2 && spaceBetweenBerriers > 5)//change 5 to verrialbe
        {
            SpawnBerrier();
        }

    }
    //spawn a rnd number of coins 
    IEnumerator SpawnCoins(int numToSpawn)
    {
        int numSpawned = 0;

        while (numSpawned < numToSpawn)
        {
            rnd = Random.Range(0, 2);
            //Debug.Log(rnd);
            newCoin = Instantiate(coin, coinSpawnPoints[rnd].transform.position, Quaternion.identity) as GameObject;//screwed shit up but not anymore
            newCoin.transform.SetParent(newGround.transform);
            ++numSpawned;
            yield return new WaitForSeconds(1);//change to a veriable so it can change while move speed changes
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
    void SpawnBerrier()
    {
        spaceBetweenBerriers = 0;
        newBerrier = Instantiate(berrier, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        newBerrier.transform.SetParent(newGround.transform);
    }
}
