using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    Rigidbody player;
    NavMeshAgent playerNav;
    NavMesh groundNavMesh;
    float moveSpeed = 5;
    float jumpHight = 4;
    bool jumping = false;


    void Awake()
    {
        player = GetComponent<Rigidbody>();
        playerNav = GetComponent<NavMeshAgent>();      
    }

    void FixedUpdate()
    {
        if (!GameManager.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !jumping)
            {
                playerNav.enabled = false;
                //player.MovePosition(transform.position + transform.up * jumpHight * Time.deltaTime);
                player.velocity = new Vector3(0, 0, 0);
                player.AddForce(Vector3.up * jumpHight, ForceMode.Impulse);//jump is not consistant
                jumping = true;
            }
            //move left and right
            else if (Input.GetKey(KeyCode.D) && !jumping)
            {
                player.MovePosition(transform.position + transform.right * moveSpeed * Time.deltaTime);
            }
            else if (Input.GetKey(KeyCode.A) && !jumping)
            {
                player.MovePosition(transform.position - transform.right * moveSpeed * Time.deltaTime);
            }
        }
    }
    
    void OnCollisionEnter(Collision other)
    {
        //when you hit the ground after jump turn navMeshAgent back on
        if (other.gameObject.tag == "ground")
        {
            playerNav.enabled = true;
            jumping = false;
        }
        if (other.gameObject.tag == "barrier")
        {
            HudEffects.instance.takeDamage(other.gameObject, 50);
            Destroy(other.gameObject);
            //Debug.Log("barrier");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //Debug.Log("got coin");
            HudEffects.score += 5;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "death")
        {
            HudEffects.instance.takeDamage(other.gameObject, 20);
        }
    }

}
