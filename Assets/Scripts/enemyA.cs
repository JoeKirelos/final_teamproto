using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyA : MonoBehaviour
{
    public GameObject playerNuke;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public Transform player;
    Vector2 bounceVec = new Vector2(1, 1);
    public Rigidbody2D rb;
    public float bounce;
    public int bounceRandom = 1;
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public GameObject self;
    public float shootInterval;
    public float randomizerInt;
    public float bounceInt;
    public float initialShoot;
    public Animator animator;
    public AudioClip spitting;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        playerNuke = GameObject.FindWithTag("Player");
        StartCoroutine(PseudoRandom());
        StartCoroutine(Bounce());
        StartCoroutine(Shoot());

        GetComponent<AudioSource>().clip = spitting;
        GetComponent<AudioSource>().playOnAwake = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
        if (playerNuke.GetComponent<Player>().nuked)
        {
            DestroySelf();
        }
    }
    IEnumerator PseudoRandom()
    {
        while (true)
        {
            yield return new WaitForSeconds(randomizerInt);
        int randomizer = Random.Range(5, 10);
        if (retreatDistance+10 >= stoppingDistance)
        {
            retreatDistance -= randomizer;
        } else if( retreatDistance+10 < stoppingDistance)
        {
            retreatDistance += randomizer;
        }
        }
    }
    IEnumerator Bounce()
    {
        while (true)
        {
            yield return new WaitForSeconds(bounceInt);
            if(bounceRandom == 1 )
            {
                bounceRandom = -bounceRandom;
            }
            else if ( bounceRandom == -1 )
            {
                bounceRandom = -bounceRandom;
            }
            rb.velocity = (bounceVec * bounceRandom * bounce * Time.deltaTime);
        }
    }
    IEnumerator Shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(shootInterval);
            animator.SetTrigger("Shoot");
            Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
            GetComponent<AudioSource>().PlayOneShot(spitting);
        }
    }

    public void DestroySelf()
    {
        player.GetComponent<Player>().EnemyKilled();
        Destroy(self);
    }
}
