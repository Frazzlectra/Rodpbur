using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    public GameObject ground;
    public GameObject column;
    public static GameManager instance;
    int rnd;//for spawning obsticles etc.
    int rndObj;
    ////obsticles
    //public GameObject deathBlock;
    //public GameObject coin;
    public GameObject[] spawnable;
    public GameObject berrier;
    //Ground speed increase
    public static float timer;
    public static bool GameOver = false;
    public static float coinDelay = .8f;

    GameObject spawnPoint;
    GameObject[] enviromentSpawnPoints;
    GameObject[] coinSpawnPoints;
    //obsticle instantiation
    GameObject newColumn;
    GameObject newCoin;
    GameObject newDeathBlock;
    GameObject newBerrier;
    Vector3 berrierSpawn;
    bool spawningCoins = false;
    bool spawningBerrier = false;
    GameObject newGround;
    int spaceBetweenBerriers;
    private Transform groundClones;
    //spawn columns?
    bool columns = true;


    void Awake()
    {        
        instance = this.gameObject.GetComponent<GameManager>();        
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
        coinSpawnPoints = GameObject.FindGameObjectsWithTag("coinSpawn");
        enviromentSpawnPoints = GameObject.FindGameObjectsWithTag("columnSpawnPoint");
        berrierSpawn = new Vector3(spawnPoint.transform.position.x, 0.8f, spawnPoint.transform.position.z);
        //Debug.Log("Coin spawn points: " + coinSpawnPoints[0] + " " + coinSpawnPoints[1]);
        groundClones = new GameObject("GroundClones").transform;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
    }
    //spawns new ground when spawn trigger is empty
    public void SpawnEnviroment()
    {
        rnd = Random.Range(0, 4);
        newGround = Instantiate(ground, spawnPoint.transform.position, Quaternion.identity ) as GameObject;
        newGround.transform.SetParent(groundClones);
        //make columns every other round
        if (columns == true)
        {
            for (int i = 0; i < enviromentSpawnPoints.Length; i++)
            {
                newColumn = Instantiate(column, enviromentSpawnPoints[i].transform.position, Quaternion.identity) as GameObject;
                newColumn.transform.SetParent(newGround.transform);
            }
            columns = false;
        }
        else
        {
            columns = true;
        }
        //get obsticales
        ++spaceBetweenBerriers;
        if (rnd <= 1 && !spawningCoins)
        {
            rnd = Random.Range(10, 15);
            StartCoroutine(SpawnCoins(rnd));
            spawningCoins = true;
        }
        //if (rnd == 1)
        //{
        //    rnd = Random.Range(0, 2);
        //    SpawnDeathBlocks(rnd);
        //}
        if (rnd == 2 && spaceBetweenBerriers > 5)//change 5 to verrialbe
        {
            spawningBerrier = true;
            SpawnBerrier();
        }
    }
    //spawn a rnd number of coins 
    IEnumerator SpawnCoins(int numToSpawn)
    {
        int numSpawned = 0;

        while (numSpawned < numToSpawn)
        {
            if (!spawningBerrier)
            {
                spawningCoins = true;
                rnd = Random.Range(0, 2);
                rndObj = Random.Range(0, 4);
                if (rndObj >= 2)
                {
                    rndObj = 0;
                }
                //Debug.Log(rnd);
                newCoin = Instantiate(spawnable[rndObj], coinSpawnPoints[rnd].transform.position, Quaternion.identity) as GameObject;//screwed shit up but not anymore
                newCoin.transform.SetParent(newGround.transform);
                ++numSpawned;
                yield return new WaitForSeconds(coinDelay);//change with move speed changes
            }
            else
            {
                yield return new WaitForSeconds(coinDelay + .3f);
            }
        }
        if (numSpawned >= numToSpawn)
        {
            spawningCoins = false;
        }
    }
    
    //void SpawnDeathBlocks(int rndPoint)
    //{
    //    //spawn new death block from coin spawnpoint
    //    newDeathBlock = Instantiate(deathBlock, coinSpawnPoints[rndPoint].transform.position, Quaternion.identity) as GameObject;
    //    newDeathBlock.transform.SetParent(newGround.transform);
    //}

    void SpawnBerrier()
    {        
        spaceBetweenBerriers = 0;
        newBerrier = Instantiate(berrier, berrierSpawn, Quaternion.identity) as GameObject;
        newBerrier.transform.SetParent(newGround.transform);
        //yield return new WaitForSeconds(.02f);
        spawningBerrier = false;
        //Debug.Break();
    }
}
