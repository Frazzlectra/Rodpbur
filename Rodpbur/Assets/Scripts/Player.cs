using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public AudioClip coinPing;
    public AudioClip deathBlock;

    Rigidbody player;
    NavMeshAgent playerNav;
    AudioSource playerAudio;
    NavMesh groundNavMesh;
    float moveSpeed = 5;
    float jumpHight = 4;
    bool jumping = false;



    void Awake()
    {
        player = GetComponent<Rigidbody>();
        playerNav = GetComponent<NavMeshAgent>();
        playerAudio = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (!GameManager.GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Debug.Log("clicked");
            }
            if (Input.GetKeyDown(KeyCode.Space) && !jumping || Input.GetKeyDown(KeyCode.Mouse0) && !jumping)
            {
                playerNav.enabled = false;
                //player.MovePosition(transform.position + transform.up * jumpHight * Time.deltaTime);
                player.velocity = new Vector3(0, 0, 0);
                player.AddForce(Vector3.up * jumpHight, ForceMode.Impulse);//jump is not consistant
                jumping = true;
                //Debug.Log("jump");
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
            //Debug.Log("grounded");
        }
        if (other.gameObject.tag == "barrier")
        {
            HudEffects.instance.takeDamage(other.gameObject, 50);
            Destroy(other.gameObject);
            playerAudio.clip = deathBlock;
            playerAudio.Play();
            //Debug.Log("barrier");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin")
        {
            //Debug.Log("got coin");
            playerAudio.clip = coinPing;
            playerAudio.Play();
            ++HudEffects.score;
            Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "death")
        {
            playerAudio.clip = deathBlock;
            playerAudio.Play();
            HudEffects.instance.takeDamage(other.gameObject, 10);
        }
    }

}
