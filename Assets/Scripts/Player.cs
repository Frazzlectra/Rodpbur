using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody player;
    NavMeshAgent playerNav;
    NavMesh groundNavMesh;
    float moveSpeed = 5;
    float jumpHight = 15;


    void Awake()
    {
        player = GetComponent<Rigidbody>();
        playerNav = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.D))
        {
            player.MovePosition(transform.position +  transform.right * moveSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            player.MovePosition(transform.position - transform.right * moveSpeed * Time.deltaTime);
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            playerNav.enabled = false;
            player.AddRelativeForce(Vector3.up * jumpHight,ForceMode.Impulse);            
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            playerNav.enabled = true;
        }
    }
}
