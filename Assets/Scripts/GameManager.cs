using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
    GameObject spawnPoint;
    public GameObject ground;
    public static GameManager instance;
    private Transform groundClones;
    GameObject newGround;

    void Awake()
    {
        instance = this.gameObject.GetComponent<GameManager>();        
        spawnPoint = GameObject.FindGameObjectWithTag("mainSpawnPoint");
        groundClones = new GameObject("GroundClones").transform;
    }

    public void SpawnGround()
    {        
        newGround = Instantiate(ground, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        newGround.transform.SetParent(groundClones);
    }
}
